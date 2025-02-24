using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using AutoFixture;

namespace TreeNodeConsoleApp.Tests;

[UseReporter(typeof(DiffReporter))]
[UseApprovalSubdirectory("Approvals")]
public class Tests
{
    [Test]
    public void Test1()
    {
        var rootGuid = Guid.Parse("f85d190b-7e6c-4ac0-b2a4-e8a864776642");
        var dep0 = 1;
        var departmentNamePart = $"Подразделение {dep0}";
        var childrenGuids = new[]
        {
            Guid.Parse("e73ddd71-44d2-430c-a45e-cd1be335f9ce"),
            Guid.Parse("e78f0cf3-9cee-4195-8d36-810b11ea5119"),
            Guid.Parse("4c647a72-91e4-483c-9362-a01aaad15ffa")
        };
        var childOneChildrenGuid = new[]
        {
            Guid.Parse("67e5bff4-ce7a-4caa-a685-792f31fc63df"),
            Guid.Parse("a3eff179-ead2-494a-9cd3-f284614ee7eb"),
            Guid.Parse("0195bc34-d6f3-4183-8987-780593831965"),
            Guid.Parse("899eaa8f-f2ed-44f0-be8f-0c0e647a2fb1")
        };
        var childTwoChildrenGuid = new[]
        {
            Guid.Parse("01aa4113-a169-41ae-9076-cc324e4fe48a"),
            Guid.Parse("30985e85-f630-45f1-95ad-989150ea9c57")
        };
        var root = new TreeNode { Id = rootGuid, Name = departmentNamePart, Children = [] };
        var childOne = CreateChild(childrenGuids[0], $"{departmentNamePart}.1", root.Id);
        var childTwo = CreateChild(childrenGuids[1], $"{departmentNamePart}.2", root.Id);
        var childThree = CreateChild(childrenGuids[2], $"{departmentNamePart}.3", root.Id);
        root.Children =
        [
            childOne,
            childTwo,
            childThree
        ];

        childOne.Children = CreateChildrenTreeNodes($"{departmentNamePart}.1", childOneChildrenGuid, childOne.Id);
        childTwo.Children = CreateChildrenTreeNodes($"{departmentNamePart}.2", childTwoChildrenGuid, childTwo.Id);

        var tree = root;
        var res = Newtonsoft.Json.JsonConvert.SerializeObject(tree);
        Approvals.VerifyJson(res);
    }

    [Test]
    public void TestCreateTree2()
    {
        var rootGuids = new[]
        {
            Guid.Parse("a84ead7c-ff90-45f6-8f71-5f49c2c0167e"),
            Guid.Parse("15f8b8f9-2c2f-4647-b90f-f9d552ecf36b"),
            Guid.Parse("ce7263bb-8318-40b6-bacc-f0292e648da1"),
            Guid.Parse("33b62d51-2a13-4907-bd76-7f4a7750007d")
        };

        var roots = new List<TreeNode>();
        var startGuid = Guid.Empty;
        for (var i = 0; i < rootGuids.Length; i++)
        {
            var childrenGuids = GenerateChildrenGuids(ref startGuid, 3);
            var departmentNamePart = $"Подразделение {i + 1}";
            var root = CreateRootTree(departmentNamePart, childrenGuids, rootGuids[i]);
            root.Children[0].Children = CreateChildrenTreeNodes($"{departmentNamePart}.1",
                GenerateChildrenGuids(ref startGuid, 4), root.Id);
            root.Children[1].Children = CreateChildrenTreeNodes($"{departmentNamePart}.1",
                GenerateChildrenGuids(ref startGuid, 2), root.Id);
            roots.Add(root);
        }

        var tree = roots;
        var res = Newtonsoft.Json.JsonConvert.SerializeObject(tree);
        Approvals.VerifyJson(res);
    }

    private static Guid[] GenerateChildrenGuids(ref Guid startGuid, int count)
    {
        var items = new List<Guid>();
        for (int i = 0; i < count; i++)
        {
            items.Add(IncrementGuid(ref startGuid));
        }

        return items.ToArray();
    }

    private static Guid IncrementGuid(ref Guid g)
    {
        var bytes = g.ToByteArray();
        bytes[^1]++;
        g = new Guid(bytes);
        return g;
    }

    private static TreeNode CreateRootTree(string departmentNamePart, Guid[] childrenGuids, Guid rootGuid)
    {
        
        var root = new TreeNode { Id = rootGuid, Name = departmentNamePart, Children = [] };
        root.Children = childrenGuids
            .Select((_, i) => CreateChild(childrenGuids[i], $"{departmentNamePart}.{i + 1}", root.Id))
            .ToArray();
        return root;
    }

    private static TreeNode[] CreateChildrenTreeNodes(string departmentNamePart, Guid[] childrenGuids, Guid parentId)
    {
        var items = new List<TreeNode>();
        for (var i = 0; i < childrenGuids.Length; i++)
        {
            items.Add(CreateChild(childrenGuids[i], $"{departmentNamePart}.{i + 1}", parentId));
        }

        return items.ToArray();
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