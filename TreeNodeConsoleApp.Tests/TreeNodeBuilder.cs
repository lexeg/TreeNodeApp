using AutoFixture;

namespace TreeNodeConsoleApp.Tests;

public static class TreeNodeBuilder
{
    public static DepartmentWithChildrenModel CreateTree(int dep0, int childrenCount, Guid rootGuid)
    {
        var root = new DepartmentWithChildrenModel { Id = rootGuid, Name = $"Подразделение {dep0}", Children = [] };
        var childOne = CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.1", root.Id);
        var childTwo = CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.2", root.Id);
        var childThree = CreateChild(Guid.NewGuid(), $"Подразделение {dep0}.3", root.Id);
        root.Children =
        [
            childOne,
            childTwo,
            childThree
        ];

        var items = new List<DepartmentWithChildrenModel>();
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

    public static DepartmentWithChildrenModel[] CreateTree2()
    {
        const int rootCount = 4;
        var guidItems = new Guid[rootCount];
        for (var i = 0; i < rootCount; i++)
        {
            guidItems[i] = Guid.NewGuid();
        }

        var roots = new List<DepartmentWithChildrenModel>();
        for (var i = 1; i <= rootCount; i++)
        {
            var root = CreateTree(i, 4, guidItems[i]);
            roots.Add(root);
        }

        return roots.ToArray();
    }

    public static DepartmentWithChildrenModel[] CreateBigTree2(int rootCount, int childrenCount)
    {
        // case 1: rootCount = 1, childrenCount = 1000
        // case 2: rootCount = 4, childrenCount = 1000
        var guidItems = new Guid[rootCount];
        for (var i = 0; i < rootCount; i++)
        {
            guidItems[i] = Guid.NewGuid();
        }

        var roots = new List<DepartmentWithChildrenModel>();
        for (var i = 0; i < rootCount; i++)
        {
            var root = CreateTree(i + 1, childrenCount, guidItems[i]);
            roots.Add(root);
        }

        return roots.ToArray();
    }

    private static DepartmentWithChildrenModel CreateChild(Guid id, string name, Guid rootId)
    {
        var fixture = new Fixture();
        var result = fixture.Build<DepartmentWithChildrenModel>()
            .With(x => x.Id, id)
            .With(x => x.Name, name)
            .With(x => x.ParentId, rootId)
            .With(x => x.Children, [])
            .Create();
        return result;
    }
}