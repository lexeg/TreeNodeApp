using TreeNodeConsoleApp.Extensions;

namespace TreeNodeConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var tree = CreateTree();
        tree.Print();
    }

    private static TreeNode CreateTree()
    {
        var root = new TreeNode { Name = "Подразделение 1", Childs = new List<TreeNode>() };
        var childOne = CreateChild("Подразделение 1.1");
        var childTwo = CreateChild("Подразделение 1.2");
        root.Childs.Add(childOne);
        root.Childs.Add(childTwo);
        root.Childs.Add(CreateChild("Подразделение 1.3"));

        childOne.Childs.Add(CreateChild("Подразделение 1.1.1"));
        childOne.Childs.Add(CreateChild("Подразделение 1.1.2"));
        childOne.Childs.Add(CreateChild("Подразделение 1.1.3"));
        childOne.Childs.Add(CreateChild("Подразделение 1.1.4"));

        childTwo.Childs.Add(CreateChild("Подразделение 1.2.1"));
        childTwo.Childs.Add(CreateChild("Подразделение 1.2.2"));
        return root;
    }

    private static TreeNode CreateChild(string name)
    {
        return new TreeNode { Name = name, Childs = new List<TreeNode>() };
    }
}