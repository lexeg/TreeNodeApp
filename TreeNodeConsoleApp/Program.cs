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
        var newTree = BuildTree(children, tree);
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
        var newTree = BuildTree(children, tree);
        Console.WriteLine();
        Console.WriteLine("Result:");
        newTree.Print();
    }

    public static TreeNode[] BuildTree(TreeNode[] departments, TreeNode[] sourceTree)
    {
        var children = new List<TreeNode>();
        foreach (var grouping in departments.GroupBy(x => x.ParentId))
        {
            var key = grouping.Key;
            var values = grouping.ToArray();
            if (key.HasValue)
            {
                var root = FindRootNode(values.First(), sourceTree);
                var last = root;
                while (last.Children.Length != 0)
                {
                    last = last.Children.Last();
                }

                /*foreach (var node in values)
                {
                    last.Children.Add(node);
                }*/
                var childrenItems = new List<TreeNode>(last.Children);
                childrenItems.AddRange(values);
                last.Children = childrenItems.ToArray();

                var findRoot = children.FirstOrDefault(x => x.Id == root.Id);
                if (findRoot != null)
                {
                    var (node, treeChildren) = FindChildPositionInTree(findRoot, root);
                    node.Children = node.Children.Concat(treeChildren).ToArray();
                    /*foreach (var item in treeChildren)
                    {
                        node.Children.Add(item);
                    }*/
                }
                else
                {
                    // rootFict.Children.Add(root);
                    children.Add(root);
                }
            }
            else
            {
                /*foreach (var node in values)
                {
                    rootFict.Children.Add(node);
                }*/
                children.AddRange(values);
            }
        }

        return children.ToArray();
    }

    private static (TreeNode department, ICollection<TreeNode> children) FindChildPositionInTree(TreeNode tree,
        TreeNode childDepartment)
    {
        var child = childDepartment.Children.Last();
        var node = tree.Children.FirstOrDefault(x => x.Id == child.Id);
        return node != null
            ? FindChildPositionInTree(node, child)
            : (department: tree, children: childDepartment.Children);
    }

    private static TreeNode FindRootNode(TreeNode treeNode, TreeNode[] sourceTrees)
    {
        return sourceTrees
            .Select(sourceTree => FindRootNode(treeNode, sourceTree))
            .FirstOrDefault(root => root != null);
    }

    private static TreeNode FindRootNode(TreeNode treeNode, TreeNode sourceTree)
    {
        if (sourceTree.Id == treeNode.ParentId)
        {
            return CloneDepartment(sourceTree);
        }

        foreach (var child in sourceTree.Children)
        {
            var result = FindRootNode(treeNode, child);
            if (result != null)
            {
                return CloneDepartment(sourceTree, [result]);
            }
        }

        return null;
    }

    private static TreeNode CloneDepartment(TreeNode departmentWithChildren, TreeNode[] children = null) =>
        new()
        {
            Id = departmentWithChildren.Id,
            ParentId = departmentWithChildren.ParentId,
            Name = departmentWithChildren.Name,
            Children = children ?? []
        };
}