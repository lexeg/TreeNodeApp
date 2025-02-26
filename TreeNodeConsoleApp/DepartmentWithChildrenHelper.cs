namespace TreeNodeConsoleApp;

internal static class DepartmentWithChildrenHelper
{
    internal static DepartmentWithChildrenModel[] BuildTree(DepartmentWithChildrenModel[] departments, DepartmentWithChildrenModel[] sourceTree)
    {
        var children = new List<DepartmentWithChildrenModel>();
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

                var childrenItems = new List<DepartmentWithChildrenModel>(last.Children);
                childrenItems.AddRange(values);
                last.Children = childrenItems.ToArray();

                var findRoot = children.FirstOrDefault(x => x.Id == root.Id);
                if (findRoot != null)
                {
                    var (node, treeChildren) = FindChildPositionInTree(findRoot, root);
                    node.Children = node.Children.Concat(treeChildren).ToArray();
                }
                else
                {
                    children.Add(root);
                }
            }
            else
            {
                children.AddRange(values);
            }
        }

        return children.ToArray();
    }

    private static DepartmentWithChildrenModel FindRootNode(DepartmentWithChildrenModel treeNode, DepartmentWithChildrenModel[] sourceTrees)
    {
        return sourceTrees
            .Select(sourceTree => FindRootNode(treeNode, sourceTree))
            .FirstOrDefault(root => root != null);
    }

    private static DepartmentWithChildrenModel FindRootNode(DepartmentWithChildrenModel treeNode,
        DepartmentWithChildrenModel sourceTree)
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

    private static (DepartmentWithChildrenModel department, ICollection<DepartmentWithChildrenModel> children)
        FindChildPositionInTree(DepartmentWithChildrenModel tree, DepartmentWithChildrenModel childDepartment)
    {
        var child = childDepartment.Children.Last();
        var node = tree.Children.FirstOrDefault(x => x.Id == child.Id);
        return node != null
            ? FindChildPositionInTree(node, child)
            : (department: tree, children: childDepartment.Children);
    }

    private static DepartmentWithChildrenModel CloneDepartment(DepartmentWithChildrenModel departmentWithChildren,
        DepartmentWithChildrenModel[] children = null) =>
        new()
        {
            Id = departmentWithChildren.Id,
            ParentId = departmentWithChildren.ParentId,
            Name = departmentWithChildren.Name,
            Children = children ?? []
        };
}