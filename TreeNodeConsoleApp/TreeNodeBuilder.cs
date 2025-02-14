namespace TreeNodeConsoleApp;

public static class TreeNodeBuilder
{
    public static TreeNode CreateTree()
    {
        var root = new TreeNode { Id = Guid.NewGuid(), Name = "Подразделение 1", Children = new List<TreeNode>() };
        var childOne = CreateChild("Подразделение 1.1", root.Id);
        var childTwo = CreateChild("Подразделение 1.2", root.Id);
        root.Children.Add(childOne);
        root.Children.Add(childTwo);
        root.Children.Add(CreateChild("Подразделение 1.3", root.Id));

        childOne.Children.Add(CreateChild("Подразделение 1.1.1", childOne.Id));
        childOne.Children.Add(CreateChild("Подразделение 1.1.2", childOne.Id));
        childOne.Children.Add(CreateChild("Подразделение 1.1.3", childOne.Id));
        childOne.Children.Add(CreateChild("Подразделение 1.1.4", childOne.Id));

        childTwo.Children.Add(CreateChild("Подразделение 1.2.1", childTwo.Id));
        childTwo.Children.Add(CreateChild("Подразделение 1.2.2", childTwo.Id));
        return root;
    }

    public static TreeNode[] CreateTree2()
    {
        const int rootCount = 4;
        var roots = new List<TreeNode>();
        for (var i = 1; i <= rootCount; i++)
        {
            var root = new TreeNode { Id = Guid.NewGuid(), Name = $"Подразделение {i}", Children = new List<TreeNode>() };
            var childOne = CreateChild($"Подразделение {i}.1", root.Id);
            var childTwo = CreateChild($"Подразделение {i}.2", root.Id);

            root.Children.Add(childOne);
            root.Children.Add(childTwo);
            root.Children.Add(CreateChild($"Подразделение {i}.3", root.Id));

            childOne.Children.Add(CreateChild($"Подразделение {i}.1.1", childOne.Id));
            childOne.Children.Add(CreateChild($"Подразделение {i}.1.2", childOne.Id));
            childOne.Children.Add(CreateChild($"Подразделение {i}.1.3", childOne.Id));
            childOne.Children.Add(CreateChild($"Подразделение {i}.1.4", childOne.Id));

            childTwo.Children.Add(CreateChild($"Подразделение {i}.2.1", childTwo.Id));
            childTwo.Children.Add(CreateChild($"Подразделение {i}.2.2", childTwo.Id));

            roots.Add(root);
        }

        return roots.ToArray();
    }

    public static TreeNode CreateBigTree()
    {
        const int childrenCount = 1000;
        var root = new TreeNode { Id = Guid.NewGuid(), Name = "Подразделение 1", Children = new List<TreeNode>() };
        var childOne = CreateChild("Подразделение 1.1", root.Id);
        var childTwo = CreateChild("Подразделение 1.2", root.Id);
        root.Children.Add(childOne);
        root.Children.Add(childTwo);
        root.Children.Add(CreateChild("Подразделение 1.3", root.Id));

        for (var i = 0; i < childrenCount; i++)
        {
            childOne.Children.Add(CreateChild($"Подразделение 1.1.{i + 1}", childOne.Id));
        }

        childTwo.Children.Add(CreateChild("Подразделение 1.2.1", childTwo.Id));
        childTwo.Children.Add(CreateChild("Подразделение 1.2.2", childTwo.Id));
        return root;
    }

    public static TreeNode[] CreateBigTree2()
    {
        const int rootCount = 4;
        const int childrenCount = 1000;
        var roots = new List<TreeNode>();
        for (var i = 1; i <= rootCount; i++)
        {
            var root = new TreeNode { Id = Guid.NewGuid(), Name = $"Подразделение {i}", Children = new List<TreeNode>() };
            var childOne = CreateChild($"Подразделение {i}.1", root.Id);
            var childTwo = CreateChild($"Подразделение {i}.2", root.Id);

            root.Children.Add(childOne);
            root.Children.Add(childTwo);
            root.Children.Add(CreateChild($"Подразделение {i}.3", root.Id));

            for (var j = 0; j < childrenCount; j++)
            {
                childOne.Children.Add(CreateChild($"Подразделение {i}.1.{j + 1}", childOne.Id));
            }
            /*childOne.Childs.Add(CreateChild($"Подразделение {i}.1.1", childOne.Id));
            childOne.Childs.Add(CreateChild($"Подразделение {i}.1.2", childOne.Id));
            childOne.Childs.Add(CreateChild($"Подразделение {i}.1.3", childOne.Id));
            childOne.Childs.Add(CreateChild($"Подразделение {i}.1.4", childOne.Id));*/

            childTwo.Children.Add(CreateChild($"Подразделение {i}.2.1", childTwo.Id));
            childTwo.Children.Add(CreateChild($"Подразделение {i}.2.2", childTwo.Id));

            roots.Add(root);
        }

        return roots.ToArray();
        /*const int childrenCount = 1000;
        var root = new TreeNode { Id = Guid.NewGuid(), Name = "Подразделение 1", Childs = new List<TreeNode>() };
        var childOne = CreateChild("Подразделение 1.1", root.Id);
        var childTwo = CreateChild("Подразделение 1.2", root.Id);
        root.Childs.Add(childOne);
        root.Childs.Add(childTwo);
        root.Childs.Add(CreateChild("Подразделение 1.3", root.Id));

        for (var i = 0; i < childrenCount; i++)
        {
            childOne.Childs.Add(CreateChild($"Подразделение 1.1.{i + 1}", childOne.Id));
        }

        childTwo.Childs.Add(CreateChild("Подразделение 1.2.1", childTwo.Id));
        childTwo.Childs.Add(CreateChild("Подразделение 1.2.2", childTwo.Id));
        return root;*/
    }
    
    private static TreeNode CreateChild(string name, Guid rootId)
    {
        return new TreeNode { Id = Guid.NewGuid(), Name = name, ParentId = rootId, Children = new List<TreeNode>() };
    }
}