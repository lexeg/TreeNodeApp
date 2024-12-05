namespace TreeNodeConsoleApp.Extensions;

internal static class TreeNodeExtensions
{
    // TODO: Переделать на Stack
    public static void Print(this TreeNode value, int indent = 0)
    {
        Console.WriteLine($"{new string(Enumerable.Repeat(' ', indent).ToArray())}{value.Name}");
        var childIndent = indent + 2;
        foreach (var child in value.Childs)
        {
            Print(child, childIndent);
        }
    }

    public static IEnumerable<TreeNode> GetChildren(this TreeNode value)
    {
        // todo: переделать на stack
        if (value == null) yield break;
        if (value.Childs == null || value.Childs.Count == 0) yield return value;
        if (value.Childs == null) yield break;
        foreach (var child in value.Childs)
        {
            foreach (var treeNode in GetChildren(child)) yield return treeNode;
        }
    }

    public static IEnumerable<TreeNode> Flatten(this TreeNode value)
    {
        if (value == null) yield break;
        // if (value.Childs == null || value.Childs.Count == 0) yield return value;
        yield return value;
        if (value.Childs == null) yield break;
        foreach (var child in value.Childs)
        {
            foreach (var treeNode in Flatten(child)) yield return treeNode;
        }
    }

    public static TreeNode Copy(this TreeNode value)
    {
        return new TreeNode
        {
            Id = value.Id,
            ParentId = value.ParentId,
            Name = value.Name,
            Childs = new List<TreeNode>()
        };
    }
}