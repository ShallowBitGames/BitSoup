namespace BitSoup;

public abstract class Requirement<ID> where ID : IEquatable<ID>
{
    public int MinimumRequired { get; set; }
    public int MaximumOptional { get; set; }

    public Requirement(int minimumRequired, int maximumOptional)
    {
        MinimumRequired = minimumRequired;
        MaximumOptional = maximumOptional;
    }

    public bool IsOptional() { return MinimumRequired == 0; }

    public abstract bool FulfilledBy(Ingredient<ID> searchIngredient, int amount);

    // Requirement a "contains" requirement b iff a fulfilled => b fulfilled
    public abstract bool Contains(Requirement<ID> requirement);
}