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
        var children = tree.GetChildren();
        foreach (var child in children)
        {
            Console.WriteLine(child.Name);
        }

        var result1 = FindRootNode(children[2], tree);
        var result2 = FindRootNode(children[5], tree);
        var result3 = FindRootNode(children[6], tree);
        var newTree = BuildTree(children, new []{tree});
        Console.WriteLine();
        Console.WriteLine("Result:");
        newTree.First().Print();
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
                var root = FindRootNode(values.First(), sourceTree.First());
                TreeNode last=root;
                while (last.Childs.Count!=0)
                {
                    last = last.Childs.Last();
                }
                foreach (var node in values)
                {
                    last.Childs.Add(node);
                }

                var findRoot = rootFict.Childs.FirstOrDefault(x => x.Id == root.Id);
                if(findRoot!=null)
                {
                    findRoot.Childs.Add(root.Childs.First());
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