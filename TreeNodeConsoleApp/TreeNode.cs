namespace TreeNodeConsoleApp;

public class TreeNode
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public ICollection<TreeNode> Childs { get; set; }
}