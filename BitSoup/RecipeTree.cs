using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace BitSoup;

public class RecipeTree<ID> where ID : IEquatable<ID>
{
    RecipeNode<ID> Root { get; set; }

    public RecipeTree(Recipe<ID> rootRec)
    {
        Root = new RecipeNode<ID>(rootRec);
        Root.Parent = null;
        Root.Children = new List<RecipeNode<ID>>();
    }

    //
    private RecipeNode<ID> CreateRecipeNode(Recipe<ID> recipe, RecipeNode<ID> baseRecipeNode)
    {
        RecipeNode<ID> newRecipeNode = new RecipeNode<ID>(recipe);
        newRecipeNode.Parent = baseRecipeNode;
        newRecipeNode.Children = new();

        baseRecipeNode.Children.Add(newRecipeNode);

        return newRecipeNode;
    }

    public bool AddRecipeNode(Recipe<ID> rec)
    {
        // root sets the base conditions for this tree
        if (!rec.IsSpecializationOf(Root.Recipe))
            return false;

        var result = SinkRecipeNode(rec, Root);

        return result != null;

    }

    private RecipeNode<ID> SinkRecipeNode(Recipe<ID> rec, RecipeNode<ID> baseRecipeNode)
    {

        // try to sink below first possible child
        // !! child order may change result 
        foreach (RecipeNode<ID> child in baseRecipeNode.Children)
        {
            if (rec.IsSpecializationOf(child.Recipe))
                return SinkRecipeNode(rec, child);
        }

        // not smaller than any child:
        // 1. gather RecipeNodes with smaller (more specific) recipes
        // 2. insert new RecipeNode between them and baseRecipeNode
        // also includes the case when baseRecipeNode is a leaf

        List<RecipeNode<ID>> smallerChildren = new();

        // collect children that are more specific
        foreach (RecipeNode<ID> child in baseRecipeNode.Children)
            if (child.Recipe.IsSpecializationOf(rec))
                smallerChildren.Add(child);


        // create new RecipeNode
        RecipeNode<ID> newRecipeNode = CreateRecipeNode(rec, baseRecipeNode);

        // reparent children
        foreach (RecipeNode<ID> child in smallerChildren)
        {
            baseRecipeNode.Children.Remove(child);
            child.Parent = newRecipeNode;
        }

        newRecipeNode.Children = smallerChildren;

        return newRecipeNode;

    }

    override public string ToString()
    {
        return Root.ToString("--", 0);
    }
    /*
    public Recipe<ID> Match(ID[] ingredients)
    {
        return MatchBelow(ingredients, Root);
    }

    
    private Recipe<ID>? MatchBelow(ID[] ingredients, RecipeNode<ID> baseRecipeNode)
    {
        if (!baseRecipeNode.Recipe.MatchesIngredients(ingredients))
            return null;

        foreach (RecipeNode<ID> child in baseRecipeNode.Children)
        {
            if (child.Recipe.MatchesIngredients(ingredients))
                return child.Recipe;
        }

        return baseRecipeNode.Recipe;
    }
    */

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
