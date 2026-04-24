namespace BitSoup;

internal struct Requirement
{
    internal Ingredient Ingredient { get; }
    internal int MinRequired { get; set; }
    internal int MaxOptional { get; set; }

    public Requirement(Ingredient ingredient, int min_required, int max_optional)
    {
        this.Ingredient = ingredient;
        this.MinRequired = min_required;
        this.MaxOptional = max_optional;
    }

    internal bool isOptional() { return MinRequired == 0; }

    internal bool fulfils_required(Ingredient ing, int amount) { return Equals(ing, Ingredient) && amount >= MinRequired; }

    internal bool fulfils_optional(Ingredient ing, int amount) { return Equals(ing, Ingredient) && (MaxOptional == -1 || amount < MaxOptional); }

    internal bool CanUse(Ingredient Ingredient)
    {
        // TODO: category match etc.
        return Equals(Ingredient, this.Ingredient);
    }

    internal bool TakeAllRequired(List<Ingredient> ingredients)
    {
        int hitsNeeded = MinRequired;

        while (hitsNeeded > 0)
        {
            int index = -1;

            for (int i = 0; i < ingredients.Count; i++)
                if (CanUse(ingredients[i]))
                    index = i;

            if (index == -1)
                return false;

            hitsNeeded--;
            ingredients.RemoveAt(index);

        }

        return true;
    }

    override public string ToString()
    {
        string min = MinRequired > 0 ? MinRequired.ToString() : "0";
        string max = MaxOptional > 0 ? "-" + MaxOptional : "";
        string str = "[" + Ingredient.ToString() + ": " + min + max + "]";
        return str;
    }


}