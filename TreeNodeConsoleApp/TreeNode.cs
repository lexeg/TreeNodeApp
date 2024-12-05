namespace TreeNodeConsoleApp;

public class TreeNode : ICloneable
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public ICollection<TreeNode> Childs { get; set; }

    public object Clone()
    {
        return new TreeNode
        {
            Id = Id,
            Name = Name.Clone() as string,
            ParentId = ParentId,
            Childs = Childs.Select(child => child.Clone() as TreeNode).ToList()
        };
    }
}