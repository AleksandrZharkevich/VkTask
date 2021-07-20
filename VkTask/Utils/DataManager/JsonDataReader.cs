using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace VkTask.Utils.DataManager
{
    public class JsonDataReader
    {
        public static T ReadProperty<T>(string pathToFile, string key)
        {
            return JObject.Parse(File.ReadAllText(pathToFile))[key].ToObject<T>();
        }

        public static T ReadObject<T>(string pathToFile)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(pathToFile));
        }
    }
}
