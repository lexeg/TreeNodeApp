using TreeNodeConsoleApp.Extensions;

namespace TreeNodeConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        FixBugCs24590(Path.Combine(Environment.CurrentDirectory, "Files", "cs24590.json"));
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

    private static TreeNode[] BuildTree(TreeNode[] children, TreeNode[] sourceTree)
    {
        var rootFict = new TreeNode { Id = Guid.Empty, Name = "Fict", ParentId = null, Childs = new List<TreeNode>() };
        var grouped = children.GroupBy(x => x.ParentId).ToArray();
        foreach (var grouping in grouped)
        {
            var key = grouping.Key;
            var values = grouping.ToArray();
            if (key.HasValue)
            {
                var root = FindRootNode(values.First(), sourceTree);
                TreeNode last = root;
                while (last.Childs.Count != 0)
                {
                    last = last.Childs.Last();
                }

                foreach (var node in values)
                {
                    last.Childs.Add(node);
                }

                var findRoot = rootFict.Childs.FirstOrDefault(x => x.Id == root.Id);
                if (findRoot != null)
                {
                    var (node, treeChildren) = FindChildPositionInTree(findRoot, root);
                    foreach (var item in treeChildren)
                    {
                        node.Childs.Add(item);
                    }
                }
                else
                {
                    rootFict.Childs.Add(root);
                }
            }
            else
            {
                foreach (var node in values)
                {
                    rootFict.Childs.Add(node);
                }
            }
        }

        return rootFict.Childs.ToArray();
    }

    private static (TreeNode node, ICollection<TreeNode> children) FindChildPositionInTree(TreeNode tree,
        TreeNode childNode)
    {
        var child = childNode.Childs.Last();
        var node = tree.Childs.FirstOrDefault(x => x.Id == child.Id);
        return node != null ? FindChildPositionInTree(node, child) : (tree, childNode.Childs);
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
            var copy = CopyTreeNode(sourceTree);
            // copy.Childs.Add(treeNode);
            return copy;
        }

        foreach (var child in sourceTree.Childs)
        {
            var result = FindRootNode(treeNode, child);
            if (result != null)
            {
                var copy = CopyTreeNode(sourceTree);
                // copy.Childs.Add(treeNode); // специально закомментировал
                copy.Childs.Add(result);
                return copy;
            }
        }

        return null;
    }

    private static TreeNode CopyTreeNode(TreeNode treeNode)
    {
        return new TreeNode
        {
            Id = treeNode.Id,
            ParentId = treeNode.ParentId,
            Name = treeNode.Name,
            Childs = new List<TreeNode>()
        };
    }
}