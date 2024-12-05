using TreeNodeConsoleApp.Extensions;

namespace TreeNodeConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var tree = CreateTree();
        tree.Print();
        Console.WriteLine();
        
        Console.WriteLine("Get children:");
        // var children = tree.GetChildren().Take(5).ToArray();
        var children = tree.GetChildren().ToArray();
        foreach (var child in children)
        {
            Console.WriteLine(child.Name);
        }

        Console.WriteLine();
        var ordered = OrderByParentId(children);
        foreach (var child in ordered)
        {
            Console.WriteLine(child.Name);
        }

        var flatten = tree.Flatten();
        var grouped = GroupByParent(ordered);
        children = GetNewLevel(grouped, flatten);
        Console.WriteLine("Next");
        foreach (var child in children)
        {
            Console.WriteLine(child.Name);
        }

        Console.WriteLine();
        ordered = OrderByParentId(children);
        foreach (var child in ordered)
        {
            Console.WriteLine(child.Name);
        }
        grouped = GroupByParent(ordered);
        children = GetNewLevel(grouped, flatten);

        /*Console.WriteLine();
        var flatten = tree.Flatten();
        foreach (var node in flatten)
        {
            Console.WriteLine(node.Name);
        }*/

        Console.WriteLine();
        var newTree = BuildTree(children, flatten);
        newTree.First().Print();
    }
    
    private static TreeNode[] GetNewLevel(Dictionary<Guid?, TreeNode[]> grouped, IEnumerable<TreeNode> flatten)
    {
        var list = new List<TreeNode>();
        foreach (var (key, value) in grouped)
        {
            if (key.HasValue)
            {
                var parent = flatten.FirstOrDefault(x => x.Id == key.Value);
                var copy = parent.Copy();
                foreach (var treeNode in value)
                {
                    copy.Childs.Add(treeNode);
                }
                list.Add(copy);
            }
        }

        return list.ToArray();
    }

    private static Dictionary<Guid?, TreeNode[]> GroupByParent(IEnumerable<TreeNode> ordered)
    {
        var result = ordered
            .Where(x => x.ParentId.HasValue)
            .GroupBy(x => x.ParentId)
            .ToDictionary(x => x.Key, x => x.ToArray());
        foreach (var (key, value) in result)
        {
            Console.WriteLine($"Key: {key}");
            foreach (var node in value)
            {
                Console.Write($"{node.Name}; ");
            }

            Console.WriteLine();
        }
        return result;
    }

    private static IEnumerable<TreeNode> OrderByParentId(TreeNode[] children)
    {
        return children.OrderBy(x => x.ParentId);
    }

    private static TreeNode CreateTree()
    {
        var root = new TreeNode { Id = Guid.NewGuid(), Name = "Подразделение 1", Childs = new List<TreeNode>() };
        var childOne = CreateChild("Подразделение 1.1", root.Id);
        var childTwo = CreateChild("Подразделение 1.2", root.Id);
        root.Childs.Add(childOne);
        root.Childs.Add(childTwo);
        root.Childs.Add(CreateChild("Подразделение 1.3", root.Id));

        childOne.Childs.Add(CreateChild("Подразделение 1.1.1", childOne.Id));
        childOne.Childs.Add(CreateChild("Подразделение 1.1.2", childOne.Id));
        childOne.Childs.Add(CreateChild("Подразделение 1.1.3", childOne.Id));
        childOne.Childs.Add(CreateChild("Подразделение 1.1.4", childOne.Id));

        childTwo.Childs.Add(CreateChild("Подразделение 1.2.1", childTwo.Id));
        childTwo.Childs.Add(CreateChild("Подразделение 1.2.2", childTwo.Id));
        return root;
    }

    private static TreeNode CreateChild(string name, Guid rootId)
    {
        return new TreeNode { Id = Guid.NewGuid(), Name = name, ParentId = rootId, Childs = new List<TreeNode>() };
    }

    private static IEnumerable<TreeNode> BuildTree(IEnumerable<TreeNode> nodes, IEnumerable<TreeNode> allNodes)
    {
        var roots = new List<TreeNode>();
        // var dict = nodes.ToDictionary(e => e.Id);
        var dict = allNodes.ToDictionary(e => e.Id);
        foreach (var node in nodes)
        {
            var currentRootNode = GetRoot(node, dict);
            
            var firstOrDefault = roots.FirstOrDefault(x => x == currentRootNode);
            if (firstOrDefault == null)
            {
                roots.Add(currentRootNode);
            }

            currentRootNode.Childs.Add(node);
        }

        return roots;
    }

    private static TreeNode GetRoot(TreeNode node, Dictionary<Guid, TreeNode> dict)
    {
        if (node.ParentId == null) return CopyTreeNode(node);
        var root = GetRoot(dict[node.ParentId.Value], dict);
        return CopyTreeNode(root);
    }

    private static TreeNode CopyTreeNode(TreeNode node)
    {
        return new TreeNode
        {
            Id = node.Id,
            ParentId = node.ParentId,
            Name = node.Name,
            Childs = new List<TreeNode>()
        };
    }
}