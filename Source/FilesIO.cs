using System.IO;
using System.Text.Json;

namespace ModelLib;

public static class FilesIO
{
    public static string BaseDataDir => Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData)
        + @"\HealthPing\Data";

    public static void SaveType<T>(T obj, string fileName)
    {
        string json = JsonSerializer.Serialize(obj);
        var url = Path.Combine(BaseDataDir, fileName);
        File.WriteAllText(url, json);
    }

    public async static Task<T> ReadType<T>(string fileName)
    {
        var deserializedObj = Activator.CreateInstance(typeof(T));
        var url = Path.Combine(BaseDataDir, fileName);
        if (!File.Exists(url))
        {
            return (T)deserializedObj;
        }
        using FileStream stream = File.OpenRead(url);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

}