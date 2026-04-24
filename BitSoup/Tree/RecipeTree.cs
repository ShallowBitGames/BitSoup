using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml;

namespace BitSoup;

internal class RecipeTree
{
    RecipeNode Root { get; set; }

    public RecipeTree(Recipe rootRec)
    {
        Root = new RecipeNode(rootRec);
        Root.Parent = null;
        Root.Children = new List<RecipeNode>();
    }


    //
    private RecipeNode createNode(Recipe rec, RecipeNode baseNode)
    {
        RecipeNode newNode = new RecipeNode(rec);
        newNode.Parent = baseNode;
        newNode.Children = new();

        baseNode.Children.Add(newNode);

        return newNode;
    }

    public bool AddNode(Recipe rec)
    {
        // root sets the base conditions for this tree
        if (!(rec < Root.Recipe))
            return false;

        var result = SinkNode(rec, Root);

        return result != null;

    }

    internal RecipeNode SinkNode(Recipe rec, RecipeNode baseNode)
    {

        // try to sink below first possible child
        // !! child order may change result 
        foreach (RecipeNode child in baseNode.Children)
        {
            if (rec < child.Recipe)
                return SinkNode(rec, child);
        }

        // not smaller than any child:
        // 1. gather nodes with smaller (more specific) recipes
        // 2. insert new node between them and baseNode
        // also includes the case when baseNode is a leaf

        List<RecipeNode> smallerChildren = new();

        // collect children that are more specific
        foreach (RecipeNode child in baseNode.Children)
            if (child.Recipe < rec)
                smallerChildren.Add(child);


        // create new node
        RecipeNode newNode = createNode(rec, baseNode);

        // reparent children
        foreach (RecipeNode child in smallerChildren)
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

    public Recipe Match(Ingredient[] ingredients)
    {
        return MatchBelow(ingredients, Root);
    }

    private Recipe? MatchBelow(Ingredient[] ingredients, RecipeNode baseNode)
    {
        if (!baseNode.Recipe.MatchesIngredients(ingredients))
            return null;

        foreach (RecipeNode child in baseNode.Children)
        {
            if (child.Recipe.MatchesIngredients(ingredients))
                return child.Recipe;
        }

        return baseNode.Recipe;
    }

    internal static RecipeTree Build(CategoryTree categoryTree, List<Ingredient> ingredients, List<Recipe> recipes)
    {
        Recipe r = new Recipe("empty", "placeholder");
        return new RecipeTree(r);
    }

}
