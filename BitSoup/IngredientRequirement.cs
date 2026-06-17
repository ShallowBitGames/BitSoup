using System;
using System.Collections.Generic;
using System.Text;

namespace BitSoup
{
    public class IngredientRequirement<ID> : Requirement<ID> where ID : IEquatable<ID>
    {
        public Ingredient<ID> Ingredient { get; }

        public IngredientRequirement(Ingredient<ID> ingredient, int minimumRequired, int maximumOptional)
            : base(minimumRequired, maximumOptional)
        {
            Ingredient = ingredient;
        }

        public override bool FulfilledBy(Ingredient<ID> searchIngredient, int amount)
        {
            // Quickly compare required amount range first
            if (amount < MinimumRequired || amount > MaximumOptional)
                return false;

            // TODO: return vector/score based on match accuracy
            // Check ingredient:

            // 1. Exact Match?
            if (searchIngredient == Ingredient)
                return true;

            // 2. Generalised Match?
            else if (Ingredient.GetAllAncestors().Contains(searchIngredient))
                return true;

            else
                return false;
        }

        public override bool Contains(Requirement<ID> requirement)
        {
            if (requirement is not IngredientRequirement<ID>)
                return false;

            var ingredientRequirement = (IngredientRequirement<ID>)requirement;

            bool ingredientsMatch = ingredientRequirement.Ingredient == this.Ingredient;
            bool ingredientIsSpecialization = ingredientRequirement.Ingredient.GetAllAncestors().Contains(this.Ingredient);

            bool amountsMatch = (ingredientRequirement.MinimumRequired >= this.MinimumRequired
                                && ingredientRequirement.MaximumOptional <= this.MaximumOptional);

            return (ingredientsMatch || ingredientIsSpecialization) && amountsMatch;
        }

    }
}
