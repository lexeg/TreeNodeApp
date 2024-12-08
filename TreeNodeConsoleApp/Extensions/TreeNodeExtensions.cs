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

    public static TreeNode[] GetChildren(this TreeNode value)
    {
        // todo: переделать на stack
        if (value == null) return [];
        if (value.Childs == null || value.Childs.Count == 0) return [value];
        var visited = new HashSet<Guid>();
        var items = new List<TreeNode>();
        var stack = new Stack<TreeNode>();
        stack.Push(value);
        while (stack.Count != 0)
        {
            var node = stack.Pop();
            visited.Add(node.Id);
            if (node.Childs.Count == 0)
            {
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
}