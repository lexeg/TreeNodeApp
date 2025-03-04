﻿using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("TreeNodeConsoleApp.Tests")]
namespace TreeNodeConsoleApp.Extensions;

internal static class TreeNodeExtensions
{
    // TODO: Переделать на Stack
    public static void Print(this DepartmentWithChildrenModel value, int indent = 0)
    {
        Console.WriteLine($"{new string(Enumerable.Repeat(' ', indent).ToArray())}{value.Name}");
        var childIndent = indent + 2;
        foreach (var child in value.Children ?? [])
        {
            Print(child, childIndent);
        }
    }

    public static void Print(this DepartmentWithChildrenModel[] values, int indent = 0)
    {
        foreach (var value in values)
        {
            value.Print(indent);
        }
    }

    public static DepartmentWithChildrenModel[] GetChildren(this DepartmentWithChildrenModel value, int? offset = null, int? limit = null)
    {
        if (value == null) return [];
        if (value.Children == null || value.Children.Length == 0) return [value];
        var visited = new HashSet<Guid>();
        var items = new List<DepartmentWithChildrenModel>();
        var stack = new Stack<DepartmentWithChildrenModel>();
        var skip = offset ?? 0;
        stack.Push(value);
        while (stack.Count != 0)
        {
            var node = stack.Pop();
            visited.Add(node.Id);
            if (node.Children.Length == 0)
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

            foreach (var child in node.Children.Reverse())
            {
                if (!visited.Contains(child.Id))
                {
                    stack.Push(child);
                }
            }
        }

        return items.ToArray();
    }

    public static DepartmentWithChildrenModel[] GetChildren(this DepartmentWithChildrenModel[] values, int? offset = null, int? limit = null)
    {
        var items = new List<DepartmentWithChildrenModel>();
        var fictiveRoot = new DepartmentWithChildrenModel
            { Id = Guid.Empty, Name = "Fictive root", ParentId = null, Children = values };
        items.AddRange(fictiveRoot.GetChildren(offset, limit));
        return items.ToArray();
    }
}