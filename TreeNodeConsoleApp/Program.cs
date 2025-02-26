using TreeNodeConsoleApp.Extensions;

namespace TreeNodeConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        // FixBugCs24590(Path.Combine(Environment.CurrentDirectory, "Files", "cs24590.json"));
        // FixBugCs25487(Path.Combine(Environment.CurrentDirectory, "Files", "1237508_good-search.json"));
        FixBugCs25487(Path.Combine(Environment.CurrentDirectory, "Files", "resp-search.json"));
        /*var tree = TreeNodeBuilder.CreateBigTree2();
        tree.Print();
        Console.WriteLine();

        Console.WriteLine("Get children:");
        var children = tree.GetChildren(offset: 990, limit: 11).ToArray();
        // var children = tree.GetChildren(offset: 8, limit: 3).ToArray();
        // var children = tree.GetChildren();
        foreach (var child in children)
        {
            Console.WriteLine(child.Name);
        }

        // var result1 = FindRootNode(children[2], tree);
        // var result2 = FindRootNode(children[5], tree);
        // var result3 = FindRootNode(children[6], tree);
        var newTree = BuildTree(children, tree);
        Console.WriteLine();
        Console.WriteLine("Result:");
        newTree.Print();*/
    }

    private static void FixBugCs24590(string filePath)
    {
        var tree = JsonConverter.Deserialize(filePath);
        tree.Print();
        Console.WriteLine();
        
        Console.WriteLine("Get children:");
        var children = tree.GetChildren().ToArray();
        foreach (var child in children)
        {
            Console.WriteLine(child.Name);
        }

        // children = children.Skip(16).Take(14).ToArray();
        var newTree = DepartmentWithChildrenHelper.BuildTree(children, tree);
        Console.WriteLine();
        Console.WriteLine("Result:");
        newTree.Print();
    }

    private static void FixBugCs25487(string filePath)
    {
        var tree = JsonConverter.Deserialize(filePath);
        tree.Print();
        Console.WriteLine();

        Console.WriteLine("Get children:");
        var children = tree.GetChildren().ToArray();
        foreach (var child in children)
        {
            Console.WriteLine(child.Name);
        }

        // children = children.Skip(16).Take(14).ToArray();
        var newTree = DepartmentWithChildrenHelper.BuildTree(children, tree);
        Console.WriteLine();
        Console.WriteLine("Result:");
        newTree.Print();
    }
}