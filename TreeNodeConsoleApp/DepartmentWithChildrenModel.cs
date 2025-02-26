using Newtonsoft.Json;

namespace TreeNodeConsoleApp;

public class DepartmentWithChildrenModel
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    [JsonProperty("Children")]
    // public ICollection<TreeNode> Children { get; set; }
    public DepartmentWithChildrenModel[] Children { get; set; }
}