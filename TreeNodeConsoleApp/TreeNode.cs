using Newtonsoft.Json;

namespace TreeNodeConsoleApp;

public class TreeNode
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    [JsonProperty("children")]
    public ICollection<TreeNode> Children { get; set; }
}