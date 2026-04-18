namespace BitSoup;

public class Recipe<ID> where ID : IEquatable<ID>
{

    ID Name;
    internal List<Requirement<ID>> Requirements = [];

    public Recipe(ID name) { Name = name; }

    public void AddRequirement(ID ingredient, int min_required, int max_optional)
    {
        Requirements.Add(new Requirement<ID>(ingredient, min_required, max_optional));
    }


    // a is smaller than b iff
    // requirements of a fulfilled => requirements of b fulfilled
    // check if each of b's requirements fits into a's requirements
    public static bool operator <(Recipe<ID> lh, Recipe<ID> rh)
    {

        List<Requirement<ID>> remainingLH = new(lh.Requirements);

        foreach (Requirement<ID> required_rh in rh.Requirements)
        {

            int matchID = remainingLH.FindIndex(required_lh => EqualityComparer<ID>.Default.Equals(required_rh.ingredient, required_lh.ingredient));
            if (matchID == -1)
            {
                if (required_rh.min_required > 0)
                    return false;

                // add a new matching requirement
                remainingLH.Add(required_rh);
                matchID = remainingLH.IndexOf(required_rh);

            }

            var match = remainingLH[matchID];

            if (match.min_required < required_rh.min_required)
                return false;

            //if (match.max_optional > required_rh.max_optional)
            //    return false;

            // not quite correct yet
            // but working on constrained examples
            match.min_required += required_rh.min_required;
            match.max_optional -= required_rh.max_optional;

            remainingLH[matchID] = match;

        }

        return true;

    }

    public static bool operator >(Recipe<ID> lh, Recipe<ID> rh)
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
    public bool MatchesIngredients(ID[] ingredients)
    {
        List<ID> ingredientsLeft = new(ingredients);

        foreach (Requirement<ID> req in Requirements)
        {
            bool fulfilled = req.TakeAllRequired(ingredientsLeft);

            if (!fulfilled)
                return false;

            return true;
        }
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
