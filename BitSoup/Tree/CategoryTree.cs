using System.Text;
using System.Xml;

namespace BitSoup;

internal class CategoryTree<ID> where ID : IConvertible
{
    private Dictionary<ID, List<ID>> categoryAncestorDict = new();

    public CategoryTree(XmlNode root)
    {
        if (root != null)
            readNode(root, new List<ID>());
    }

    // adds hierarchy information
    // avoids double entries
    void addToCategories(ID id, List<ID> parentCategories)
    {
        // sibling nodes can share a list, they have the same ancestors
        if (!categoryAncestorDict.ContainsKey(id))
            categoryAncestorDict.Add(id, parentCategories);
        else
        {
            foreach (ID category in parentCategories)
                if (!categoryAncestorDict[id].Contains(category))
                    categoryAncestorDict[id].Add(category);
        }
    }

    void readNode(XmlNode node, List<ID> parentCategories)
    {

        // !!!
        // For this to work, the type of ID must be something
        // that can internally be constructed from a string
        string idAttr = ((XmlElement)node).GetAttribute("id");
        ID id = (ID)Convert.ChangeType(idAttr, typeof(ID));

        addToCategories(id, parentCategories);

        List<ID> newParents = new List<ID>(parentCategories);
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
            foreach (ID parent_category in pair.Value)
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