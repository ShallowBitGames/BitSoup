namespace BitSoup;

internal class RecipeNode<ID> where ID : IEquatable<ID>
{
    public Recipe<ID> Recipe { get; }
    public RecipeNode<ID> Parent { get; set; }
    public List<RecipeNode<ID>> Children { get; set; }

    internal RecipeNode(Recipe<ID> recipe) { Recipe = recipe; }

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
