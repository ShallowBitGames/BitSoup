using System;
using System.Collections.Generic;
using System.Text;

namespace BitSoup
{
    public class Category<ID> where ID : IEquatable<ID>
    {
        public ID Id { get; set; }
        public List<Category<ID>> Parents { get; set; }
        public bool IsBaseCategory() { return Parents.Count == 0; }

        public List<Category<ID>> GetAllAncestors()
        {
            List<Category<ID>> result = new List<Category<ID>>(Parents);
            
            foreach(Category<ID> c in Parents)
                result.AddRange(c.GetAllAncestors());

            return result;
        }
    }
}
