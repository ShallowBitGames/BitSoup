namespace BitSoup;

internal class RecipeNode
{
    public Recipe Recipe { get; }
    public RecipeNode Parent { get; set; }
    public List<RecipeNode> Children { get; set; }

    internal RecipeNode(Recipe recipe) { Recipe = recipe; }

    public string ToString(string symbol, int level)
    {
        string str = String.Concat(Enumerable.Repeat(symbol, level));
        str += " ";
        str += Recipe.ToString();
        str += Environment.NewLine;

        foreach (var child in Children)
            str += child.ToString(symbol, level + 1);

        return str;
    }
}
