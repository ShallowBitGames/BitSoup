using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;

namespace BitSoup;

public class Cooker(string categoryXmlPath, string ingredientJsonPath, string recipeJsonPath)
{
    public RecipeTree RecipeTree { get; private set; }

    private string _categoryXmlPath = categoryXmlPath;
    private string _ingredientJsonPath = ingredientJsonPath;
    private string _recipeJsonPath = recipeJsonPath;
    private bool _loaded = false;

    private async void Load()
    {
        
        
    }

    public Cooker(XmlDocument categoryXml, JsonDocument ingredientJson, JsonDocument recipeJson)
    {
        CategoryTree categoryTree = LoadCategories(categoryXml);
        List<Ingredient> ingredients = LoadIngredients(ingredientJson);
        List<Recipe> recipes = LoadRecipes(recipeJson);
        
        RecipeTree = RecipeTree.Build(categories, ingredients, recipes);
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
