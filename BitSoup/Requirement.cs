namespace BitSoup;

internal struct Requirement
{
    internal Ingredient ingredient { get; }
    internal int min_required { get; set; }
    internal int max_optional { get; set; }

    public Requirement(Ingredient ingredient, int min_required, int max_optional)
    {
        this.ingredient = ingredient;
        this.min_required = min_required;
        this.max_optional = max_optional;
    }

    internal bool isOptional() { return min_required == 0; }

    internal bool fulfils_required(ID ing, int amount) { return Equals(ing, ingredient) && amount >= min_required; }

    internal bool fulfils_optional(ID ing, int amount) { return Equals(ing, ingredient) && (max_optional == -1 || amount < max_optional); }

    internal bool CanUse(ID ingredient)
    {
        // TODO: category match etc.
        return Equals(ingredient, this.ingredient);
    }

    internal bool TakeAllRequired(List<ID> ingredients)
    {
        int hitsNeeded = min_required;

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
        string min = min_required > 0 ? min_required.ToString() : "0";
        string max = max_optional > 0 ? "-" + max_optional : "";
        string str = "[" + ingredient.ToString() + ": " + min + max + "]";
        return str;
    }


}