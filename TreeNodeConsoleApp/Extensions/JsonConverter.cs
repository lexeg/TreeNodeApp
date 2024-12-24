using Newtonsoft.Json;

namespace TreeNodeConsoleApp.Extensions;

public static class JsonConverter
{
    public static TreeNode[] Deserialize(string fileName)
    {
        var text = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<TreeNode[]>(text);
    }
}