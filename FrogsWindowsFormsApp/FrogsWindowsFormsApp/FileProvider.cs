using System.IO;
using System.Text;

namespace FrogsWindowsFormsApp
{
    public class FileProvider
    {
        public static void Set(string path, string value)
        {
            var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.WriteLine(value);
            writer.Close();
        }

        public static string Get(string path)
        {
            var reader = new StreamReader(path);
            var value = reader.ReadToEnd();
            reader.Close();
            return value;
        }

        public static bool IsExists(string path)
        {
            return File.Exists(path);
        }
    }
}
