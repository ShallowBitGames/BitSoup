using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace BitSoup;

public class RecipeTree<ID> where ID : IEquatable<ID>
{
    Node<ID> Root { get; set; }

    public RecipeTree(Recipe<ID> rootRec)
    {
        Root = new Node<ID>(rootRec);
        Root.Parent = null;
        Root.Children = new List<Node<ID>>();
    }

    //
    private Node<ID> createNode(Recipe<ID> rec, Node<ID> baseNode)
    {
        Node<ID> newNode = new Node<ID>(rec);
        newNode.Parent = baseNode;
        newNode.Children = new();

        baseNode.Children.Add(newNode);

        return newNode;
    }

    public bool AddNode(Recipe<ID> rec)
    {
        // root sets the base conditions for this tree
        if (!(rec < Root.Recipe))
            return false;

        var result = SinkNode(rec, Root);

        return result != null;

    }

    internal Node<ID> SinkNode(Recipe<ID> rec, Node<ID> baseNode)
    {

        // try to sink below first possible child
        // !! child order may change result 
        foreach (Node<ID> child in baseNode.Children)
        {
            if (rec < child.Recipe)
                return SinkNode(rec, child);
        }

        // not smaller than any child:
        // 1. gather nodes with smaller (more specific) recipes
        // 2. insert new node between them and baseNode
        // also includes the case when baseNode is a leaf

        List<Node<ID>> smallerChildren = new();

        // collect children that are more specific
        foreach (Node<ID> child in baseNode.Children)
            if (child.Recipe < rec)
                smallerChildren.Add(child);


        // create new node
        Node<ID> newNode = createNode(rec, baseNode);

        // reparent children
        foreach (Node<ID> child in smallerChildren)
        {
            baseNode.Children.Remove(child);
            child.Parent = newNode;
        }

        newNode.Children = smallerChildren;

        return newNode;

    }

    override public string ToString()
    {
        return Root.ToString("--", 0);
    }

    public Recipe<ID> Match(ID[] ingredients)
    {
        return MatchBelow(ingredients, Root);
    }

    private Recipe<ID>? MatchBelow(ID[] ingredients, Node<ID> baseNode)
    {
        if (!baseNode.Recipe.MatchesIngredients(ingredients))
            return null;

        foreach (Node<ID> child in baseNode.Children)
        {
            if (child.Recipe.MatchesIngredients(ingredients))
                return child.Recipe;
        }

        return baseNode.Recipe;
    }


    //string serializeDown()
    //{


    //}

    //public bool writeRecipesToJSON(string path)
    //{
    //    foreach 
    //        FileStream fstream = File.Create(path + "");
    //        JsonSerializer.SerializeAsync()
    //}
}
