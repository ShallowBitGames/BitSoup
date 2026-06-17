namespace BitSoup;

public class Ingredient<ID> where ID : IEquatable<ID>
{
    public ID Id { get; set; }
    public string Name { get; set; }
    public List<Category<ID>> Categories { get; set; }
    public Ingredient<ID> Parent { get; set; }

    public List<Ingredient<ID>> GetAllAncestors()
    {
        if (Parent is null)
            return [];

        List<Ingredient<ID>> result = Parent.GetAllAncestors();
        result.Add(Parent);
        return result;
    }

}