namespace BitSoup;

public class Recipe<ID> where ID : IEquatable<ID>
{

    public ID Id { get; set; }
    public string Name { get; set; }

    public List<Requirement<ID>> Requirements = [];

    public Recipe(ID id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddIngredientRequirement(Ingredient<ID> ingredient, int minimumRequired, int maximumOptional)
    {
        Requirements.Add(new IngredientRequirement<ID>(ingredient, minimumRequired, maximumOptional));
    }

    // If all requirements of the passed recipe are fulfilled,
    // all requirements of this recipe are also fulfilled
    // for now: only check for identical requirements, use each requirement once max
    public bool IsSpecializationOf(Recipe<ID> recipe)
    {
        // Copy requirements in order to 
        List<Requirement<ID>> remaining = new List<Requirement<ID>>(recipe.Requirements);

        foreach(var thisRequirement in this.Requirements)
        {
            bool found = false;
            foreach(var generalRequirement in remaining)
            {
                if (generalRequirement.Contains(thisRequirement))
                {
                    remaining.Remove(generalRequirement);
                    found = true;
                    break;
                }
            }
            if (!found)
                return false;
        }

        return true;
    }

    // a is smaller than b iff
    // requirements of a fulfilled => requirements of b fulfilled
    // check if each of b's requirements fits into a's requirements
    /*
    public static bool operator <(Recipe<ID> lh, Recipe<ID> rh)
    {

        List<Requirement<ID>> remainingLH = new(lh.Requirements);

        foreach (Requirement<ID> required_rh in rh.Requirements)
        {

            int matchID = remainingLH.FindIndex(required_lh => EqualityComparer<ID>.Default.Equals(required_rh.ingredient, required_lh.ingredient));
            if (matchID == -1)
            {
                if (required_rh.MinimumRequired > 0)
                    return false;

                // add a new matching requirement
                remainingLH.Add(required_rh);
                matchID = remainingLH.IndexOf(required_rh);

            }

            var match = remainingLH[matchID];

            if (match.MinimumRequired < required_rh.MinimumRequired)
                return false;

            //if (match.max_optional > required_rh.max_optional)
            //    return false;

            // not quite correct yet
            // but working on constrained examples
            match.MinimumRequired += required_rh.MinimumRequired;
            match.MaximumOptional -= required_rh.MaximumOptional;

            remainingLH[matchID] = match;

        }

        return true;

    }

    public static bool operator >(Recipe<ID> lh, Recipe<ID> rh)
    {
        return rh < lh;
    }
    */

    public override string ToString()
    {
        string s = Name + ": {";

        foreach (var req in Requirements)
            s += req.ToString();

        s += "}";

        return s;
    }

}
