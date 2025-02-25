using AutoFixture;

namespace TreeNodeConsoleApp.Tests;

public static class TreeNodeBuilder
{
    public static TreeNode CreateTree(int dep0, int childrenCount, Guid rootGuid)
    {
        var root = new TreeNode { Id = rootGuid, Name = $"Подразделение {dep0}", Children = [] };
        var childOne = CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.1", root.Id);
        var childTwo = CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.2", root.Id);
        var childThree = CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.3", root.Id);
        root.Children =
        [
            childOne,
            childTwo,
            childThree
        ];

        var items = new List<TreeNode>();
        for (var i = 0; i < childrenCount; i++)
        {
            items.Add(CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.1.{i + 1}", childOne.Id));
        }

        childOne.Children = items.ToArray();

        childTwo.Children =
        [
            CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.2.1", childTwo.Id),
            CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.2.2", childTwo.Id)
        ];
        return root;
    }

    public static TreeNode[] CreateTree2()
    {
        const int rootCount = 4;
        var guidItems = new Guid[rootCount];
        for (var i = 0; i < rootCount; i++)
        {
            guidItems[i] = Guid.NewGuid();
        }

        var roots = new List<TreeNode>();
        for (var i = 1; i <= rootCount; i++)
        {
            var root = CreateTree(i, 4, guidItems[i]);
            roots.Add(root);
        }

        return roots.ToArray();
    }

    public static TreeNode[] CreateBigTree2(int rootCount, int childrenCount)
    {
        // case 1: rootCount = 1, childrenCount = 1000
        // case 2: rootCount = 4, childrenCount = 1000
        var guidItems = new Guid[rootCount];
        for (var i = 0; i < rootCount; i++)
        {
            guidItems[i] = Guid.NewGuid();
        }

        var roots = new List<TreeNode>();
        for (var i = 0; i < rootCount; i++)
        {
            var root = CreateTree(i + 1, childrenCount, guidItems[i]);
            roots.Add(root);
        }

        return roots.ToArray();
    }

    private static TreeNode CreateChild(Guid id, string name, Guid rootId)
    {
        var fixture = new Fixture();
        var result = fixture.Build<TreeNode>()
            .With(x => x.Id, id)
            .With(x => x.Name, name)
            .With(x => x.ParentId, rootId)
            .With(x => x.Children, [])
            .Create();
        return result;
    }
}