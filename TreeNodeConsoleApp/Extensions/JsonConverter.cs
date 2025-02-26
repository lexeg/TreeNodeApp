using Newtonsoft.Json;

namespace TreeNodeConsoleApp.Extensions;

public static class JsonConverter
{
    public static DepartmentWithChildrenModel[] Deserialize(string fileName)
    {
        var text = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<DepartmentWithChildrenModel[]>(text);
    }
}