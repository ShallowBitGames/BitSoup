namespace BitSoup;

public class Recipe(string key, string name)
{
    public string Key { get; } = key;
    public string Name { get; } = name;
    internal List<Requirement> Requirements = [];


    public void AddRequirement(Ingredient ingredient, int MinRequired, int MaxOptional)
    {
        Requirements.Add(new Requirement(ingredient, MinRequired, MaxOptional));
    }


    // a is smaller than b iff
    // requirements of a fulfilled => requirements of b fulfilled
    // check if each of b's requirements fits into a's requirements
    public static bool operator <(Recipe lh, Recipe rh)
    {

        List<Requirement> remainingLH = new(lh.Requirements);

        foreach (Requirement required_rh in rh.Requirements)
        {

            int matchID = remainingLH.FindIndex(required_lh => EqualityComparer<string>.Default.Equals(required_rh.Ingredient.Key, required_lh.Ingredient.Key));
            if (matchID == -1)
            {
                if (required_rh.MinRequired > 0)
                    return false;

                // add a new matching requirement
                remainingLH.Add(required_rh);
                matchID = remainingLH.IndexOf(required_rh);

            }

            var match = remainingLH[matchID];

            if (match.MinRequired < required_rh.MinRequired)
                return false;

            //if (match.MaxOptional > required_rh.MaxOptional)
            //    return false;

            // not quite correct yet
            // but working on constrained examples
            match.MinRequired += required_rh.MinRequired;
            match.MaxOptional -= required_rh.MaxOptional;

            remainingLH[matchID] = match;

        }

        return true;

    }

    public static bool operator >(Recipe lh, Recipe rh)
    {
        return rh < lh;
    }


    public override string ToString()
    {
        string s = Name + ": {";

        foreach (var req in Requirements)
            s += req.ToString();

        s += "}";

        return s;
    }



    // TODO: implement recipe-ingredient match
    public bool MatchesIngredients(Ingredient[] ingredients)
    {
        List<Ingredient> ingredientsLeft = new(ingredients);

        foreach (Requirement req in Requirements)
        {
            bool fulfilled = req.TakeAllRequired(ingredientsLeft);

            if (!fulfilled)
                return false;

        }
        return true;
    }


    /*
     * (score, recipe) bestmatch = root
     * stack = root
     * 
     * while stack not empty:
     *   node = stack.pop
     *   bestmatch = node.match(ref stack, bestmatch)
     * 
     * return bestmatch.recipe
     */

    /*
     * 
     * for every child:
     *   matches:
     *      push on stack
     *   
     * score = calculate match score
     * if score > bestmatch.score:
     *   return (score, this)
     * 
     * return bestmatch
     * 
     */
}
