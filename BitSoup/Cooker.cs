using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;

namespace BitSoup;

public class Cooker
{
    private RecipeTree _recipeTree;

    public Cooker(XmlDocument categoryXml, JsonDocument ingredientJson, JsonDocument recipeJson)
    {
        CategoryTree categoryTree = LoadCategories(categoryXml);
        List<Ingredient> ingredients = LoadIngredients(ingredientJson);
        List<Recipe> recipes = LoadRecipes(recipeJson);
        
        _recipeTree = RecipeTree.Build(categoryTree, ingredients, recipes);
    }

    private CategoryTree LoadCategories(XmlDocument xmlDocument)
    {
        XmlNode? root = xmlDocument.DocumentElement;
        CategoryTree categoryTree = new(root);
        return categoryTree;
    }

    private List<Ingredient> LoadIngredients(JsonDocument jsonDocument)
    {
        
    }
    
    private List<Recipe> LoadRecipes(JsonDocument jsonDocument)
    {
        
    }

    //public Recipe FindBestMatch(IEnumerable<Ingredient> ingredients);

}
