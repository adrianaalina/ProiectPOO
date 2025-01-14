using System.Net;
using System.IO;
using System.Text.Json;
namespace Utilities.FileHelper
{

    public static class FileHelper
    {
        public static void SalvareDate<T>(string caleFisier, List<T> date)
        {
            var json= JsonSerializer.Serialize(date,new JsonSerializerOptions { WriteIndented = true });
            
        }

        public static List<T> IncarcareDate<T>(string caleFisier)
        {
            if(!File.Exists(caleFisier))
                return new List<T>();
            
            var json = File.ReadAllText(caleFisier);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

    }
}