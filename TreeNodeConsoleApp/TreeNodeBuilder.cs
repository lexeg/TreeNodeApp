namespace TreeNodeConsoleApp;

public static class TreeNodeBuilder
{
    public static DepartmentWithChildrenModel CreateTree()
    {
        var root = new DepartmentWithChildrenModel { Id = Guid.NewGuid(), Name = "Подразделение 1", Children = [] };
        var childOne = CreateChild("Подразделение 1.1", root.Id);
        var childTwo = CreateChild("Подразделение 1.2", root.Id);
        root.Children =
        [
            childOne,
            childTwo,
            CreateChild("Подразделение 1.3", root.Id)
        ];

        childOne.Children =
        [
            CreateChild("Подразделение 1.1.1", childOne.Id),
            CreateChild("Подразделение 1.1.2", childOne.Id),
            CreateChild("Подразделение 1.1.3", childOne.Id),
            CreateChild("Подразделение 1.1.4", childOne.Id)
        ];

        childTwo.Children =
        [
            CreateChild("Подразделение 1.2.1", childTwo.Id),
            CreateChild("Подразделение 1.2.2", childTwo.Id)
        ];
        return root;
    }

    public static DepartmentWithChildrenModel[] CreateTree2()
    {
        const int rootCount = 4;
        var roots = new List<DepartmentWithChildrenModel>();
        for (var i = 1; i <= rootCount; i++)
        {
            var root = new DepartmentWithChildrenModel { Id = Guid.NewGuid(), Name = $"Подразделение {i}", Children = [] };
            var childOne = CreateChild($"Подразделение {i}.1", root.Id);
            var childTwo = CreateChild($"Подразделение {i}.2", root.Id);

            root.Children =
            [
                childOne,
                childTwo,
                CreateChild($"Подразделение {i}.3", root.Id)
            ];

            childOne.Children =
            [
                CreateChild($"Подразделение {i}.1.1", childOne.Id),
                CreateChild($"Подразделение {i}.1.2", childOne.Id),
                CreateChild($"Подразделение {i}.1.3", childOne.Id),
                CreateChild($"Подразделение {i}.1.4", childOne.Id)
            ];

            childTwo.Children =
            [
                CreateChild($"Подразделение {i}.2.1", childTwo.Id),
                CreateChild($"Подразделение {i}.2.2", childTwo.Id)
            ];

            roots.Add(root);
        }

        return roots.ToArray();
    }

    public static DepartmentWithChildrenModel CreateBigTree()
    {
        const int childrenCount = 1000;
        var root = new DepartmentWithChildrenModel { Id = Guid.NewGuid(), Name = "Подразделение 1", Children = [] };
        var childOne = CreateChild("Подразделение 1.1", root.Id);
        var childTwo = CreateChild("Подразделение 1.2", root.Id);
        root.Children =
        [
            childOne,
            childTwo,
            CreateChild("Подразделение 1.3", root.Id)
        ];

        var items = new List<DepartmentWithChildrenModel>();
        for (var i = 0; i < childrenCount; i++)
        {
            items.Add(CreateChild($"Подразделение 1.1.{i + 1}", childOne.Id));
        }

        childOne.Children = items.ToArray();

        childTwo.Children =
        [
            CreateChild("Подразделение 1.2.1", childTwo.Id),
            CreateChild("Подразделение 1.2.2", childTwo.Id)
        ];
        return root;
    }

    public static DepartmentWithChildrenModel[] CreateBigTree2()
    {
        const int rootCount = 4;
        const int childrenCount = 1000;
        var roots = new List<DepartmentWithChildrenModel>();
        for (var i = 1; i <= rootCount; i++)
        {
            var root = new DepartmentWithChildrenModel { Id = Guid.NewGuid(), Name = $"Подразделение {i}", Children = [] };
            var childOne = CreateChild($"Подразделение {i}.1", root.Id);
            var childTwo = CreateChild($"Подразделение {i}.2", root.Id);

            root.Children =
            [
                childOne,
                childTwo,
                CreateChild($"Подразделение {i}.3", root.Id)
            ];

            var items = new List<DepartmentWithChildrenModel>();
            for (var j = 0; j < childrenCount; j++)
            {
                items.Add(CreateChild($"Подразделение {i}.1.{j + 1}", childOne.Id));
            }

            childOne.Children = items.ToArray();
            /*childOne.Childs.Add(CreateChild($"Подразделение {i}.1.1", childOne.Id));
            childOne.Childs.Add(CreateChild($"Подразделение {i}.1.2", childOne.Id));
            childOne.Childs.Add(CreateChild($"Подразделение {i}.1.3", childOne.Id));
            childOne.Childs.Add(CreateChild($"Подразделение {i}.1.4", childOne.Id));*/

            childTwo.Children =
            [
                CreateChild($"Подразделение {i}.2.1", childTwo.Id),
                CreateChild($"Подразделение {i}.2.2", childTwo.Id)
            ];

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
    
    private static DepartmentWithChildrenModel CreateChild(string name, Guid rootId)
    {
        return new DepartmentWithChildrenModel { Id = Guid.NewGuid(), Name = name, ParentId = rootId, Children = [] };
    }
}