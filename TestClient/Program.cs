// See https://aka.ms/new-console-template for more information
using BitSoup;
using System.Xml;

Console.WriteLine("Hello, World! Let's get cooking!");

const string categoriesPath = "TestData/categories.xml";
const string ingredientsPath = "TestData/";
const string recipesPath = "TestData/potions.json";


while (true)
{
    Console.WriteLine("SELECT: [1] Cook,  [2] Print Data, [3] Choose different files, [ESC] Exit");

    char pressedkey = Console.ReadKey().KeyChar;

    switch (pressedkey)
    {
        case '1':
            break;
        
        case '2':
            break;
        
        case '3':
            break;

        case ((char)ConsoleKey.Escape):
            return;
    }
}

/*
string path { get; set; } = "";
Recipe<string> tea = new("Tea");
tea.AddRequirement("Water", 1, 3);
//            tea.AddRequirement("Apple", 0, 3);
//tea.AddRequirement("Fennel", 0, 3);
//tea.AddRequirement("Anise", 0, 3);
//tea.AddRequirement("Cumin", 0, 3);
//tea.AddRequirement("Cinnamon", 0, 3);

Recipe<string> fenneltea = new("Fennel Tea");
fenneltea.AddRequirement("Water", 1, 3);
fenneltea.AddRequirement("Fennel", 1, 3);

Recipe<string> appletea = new("Apple Tea");
appletea.AddRequirement("Water", 1, 3);
appletea.AddRequirement("Apple", 1, 3);
//appletea.AddRequirement("Anise", 0, 3);
//appletea.AddRequirement("Cinnamon", 0, 3);

Recipe<string> factea = new("Fenchel Anis Kümmööööl");
factea.AddRequirement("Water", 1, 3);
factea.AddRequirement("Fennel", 1, 3);
factea.AddRequirement("Anise", 1, 3);
factea.AddRequirement("Cumin", 1, 3);

Recipe<string> actea = new("Apple Cinnamon Tea");
actea.AddRequirement("Water", 1, 3);
actea.AddRequirement("Apple", 1, 3);
//actea.AddRequirement("Anise", 0, 1);
actea.AddRequirement("Cinnamon", 1, 3);

Recipe<string> fatea = new("Fennel Apple Tea");
fatea.AddRequirement("Water", 1, 3);
fatea.AddRequirement("Apple", 1, 3);
fatea.AddRequirement("Fennel", 1, 3);
//fatea.AddRequirement("Anise", 0, 1);
//fatea.AddRequirement("Cumin", 0, 1);
//fatea.AddRequirement("Cinnamon", 0, 1);

//Console.WriteLine(factea.ToString());

RecipeTree<string> rt = new(tea);
rt.AddNode(appletea);
rt.AddNode(actea);
rt.AddNode(factea);
rt.AddNode(fatea);
rt.AddNode(fenneltea);

Console.Write(rt.ToString());

//rt.writeToJSON(path + "tea.json");

Categories<string> cats = new(path + "categories.xml");
Console.WriteLine(cats.ToString());
*/