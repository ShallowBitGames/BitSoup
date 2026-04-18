namespace BitSoup;

public class Cooking
{

    public Recipe FindBestMatch(IEnumerable<Ingredient> ingredients);

    public RecipeTree RecipeTree { get; }


    public async Cooking(string categoryXmlPath, string ingredientJsonPath, string recipeJsonPath)
    {
        CategoryTree categories;
        IEnumerable<Ingredient> ingredients;
        IEnumerable<Recipe> recipes;

        Task categoryTask = async () => categories = await LoadCategories(categoryXmlPath);
        Task ingredientTask = async () => ingredients = await LoadIngredients(ingredientJsonPath);
        Task recipeTask = async () => recipes = await LoadRecipes(recipeJsonPath);

        Task.WaitAll(categoryTask, ingredientTask, recipeTask);

        recipeTree = RecipeTree.Build(categories, ingredients, recipes);
    }

    private async CategoryTree LoadCategories(string xmlFilePath)
    {
        
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(path);

        XmlNode? root = xmlDocument.DocumentElement;
    }

    private async IEnumerable<Ingredient> LoadIngredients(string jsonFilePath)
    {
        
    }
    
    private async IEnumerable<Recipe> LoadRecipes(string jsonFilePath)
    {
        
    }

}
