using System.Text;
using System.Xml;

namespace BitSoup;

internal class CategoryTree
{
    private Dictionary<Category, List<Category>> categoryAncestorDict = new();

    public CategoryTree(XmlNode root)
    {
        if (root != null)
            readNode(root, new List<Category>());
    }

    // adds hierarchy information
    // avoids double entries
    void addToCategories(Category category, List<Category> parentCategories)
    {
        // sibling nodes can share a list, they have the same ancestors
        if (!categoryAncestorDict.ContainsKey(category))
            categoryAncestorDict.Add(category, parentCategories);
        else
        {
            foreach (Category cat in parentCategories)
                if (!categoryAncestorDict[category].Contains(category))
                    categoryAncestorDict[category].Add(category);
        }
    }

    void readNode(XmlNode node, List<Category> parentCategories)
    {

        // !!!
        // For this to work, the type of Category must be something
        // that can internally be constructed from a string

        addToCategories(id, parentCategories);

        List<Category> newParents = new List<Category>(parentCategories);
        newParents.Add(id);
        if (node.HasChildNodes)
            foreach (XmlNode childNode in node.ChildNodes)
                readNode(childNode, newParents);

    }

    override public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var pair in categoryAncestorDict)
        {
            sb.Append(pair.Key.ToString() + " is a: ");
            foreach (Category parent_category in pair.Value)
                sb.Append(parent_category + ", ");

            sb.Remove(sb.Length - 2, 2);
            sb.Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    //void add(string path)


    // TODO
    // 
    //bool validateHierarchy()

}