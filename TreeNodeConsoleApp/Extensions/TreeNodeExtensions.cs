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
}