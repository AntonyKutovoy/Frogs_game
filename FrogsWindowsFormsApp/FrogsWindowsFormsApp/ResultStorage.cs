using System.Collections.Generic;
using Newtonsoft.Json;

namespace FrogsWindowsFormsApp
{
    public class ResultStorage
    {
        private static string resultPath = "result.json";

        public static List<int> GetResultFromFile()
        {
            var serializedResult = FileProvider.Get(resultPath);
            var result = JsonConvert.DeserializeObject<List<int>>(serializedResult);
            return result;
        }

        public static void SaveResult(List<int> result)
        {
            var serializedResult = JsonConvert.SerializeObject(result, Formatting.Indented);
            FileProvider.Set(resultPath, serializedResult);
        }
    }
}
