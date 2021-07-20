using System.IO;

namespace VkTask.Utils.DataManager
{
    public class TextDataReader
    {
        public static string ReadText(string pathToFile) => File.ReadAllText(pathToFile);
    }
}
