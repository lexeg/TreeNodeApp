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

    public static void Print(this TreeNode[] values, int indent = 0)
    {
        foreach (var value in values)
        {
            value.Print(indent);
        }
    }

    public static TreeNode[] GetChildren(this TreeNode value, int? offset = null, int? limit = null)
    {
        // todo: переделать на stack
        if (value == null) return [];
        if (value.Childs == null || value.Childs.Count == 0) return [value];
        var visited = new HashSet<Guid>();
        var items = new List<TreeNode>();
        var stack = new Stack<TreeNode>();
        var skip = offset ?? 0;
        stack.Push(value);
        while (stack.Count != 0)
        {
            var node = stack.Pop();
            visited.Add(node.Id);
            if (node.Childs.Count == 0)
            {
                if (skip != 0)
                {
                    skip--;
                    continue;
                }

                if (limit != null && items.Count >= limit)
                {
                    break;
                }

                items.Add(node);
            }

            foreach (var child in node.Childs.Reverse())
            {
                if (!visited.Contains(child.Id))
                {
                    stack.Push(child);
                }
            }
        }

        return items.ToArray();
    }

    public static TreeNode[] GetChildren(this TreeNode[] values, int? offset = null, int? limit = null)
    {
        var items = new List<TreeNode>();
        var rootFict = new TreeNode { Id = Guid.Empty, Name = "Fict", ParentId = null, Childs = new List<TreeNode>(values) };
        /*foreach (var value in values)
        {
            items.AddRange(value.GetChildren(offset, limit));
        }*/
        items.AddRange(rootFict.GetChildren(offset, limit));
        return items.ToArray();
    }
}