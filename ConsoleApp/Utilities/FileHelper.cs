using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;
namespace Utilities.FileHelper
{
    public static class FileHelper
    {
            public static void SalvareDate<T>(string caleFisier, List<T> date)
            {
                using (var writer = new StreamWriter(caleFisier))
                {
                    foreach (var item in date)
                    {

                        if (item is Utilizator utilizator)
                        {
                            string tipUtilizator = "student";
                            if (utilizator.Username.Contains("admin"))
                            {
                                tipUtilizator = "admin";
                            }
                            else if (utilizator.Username.Contains("prof"))
                            {
                                tipUtilizator = "profesor";
                                writer.WriteLine(
                                    $"{utilizator.Id},{utilizator.Nume},{utilizator.Username},{utilizator.Parola},{tipUtilizator}");
                            }
                        }
                        else
                        {
                            var properties = typeof(T).GetProperties();
                            var propList = properties.Select(p => p.GetValue(item)?.ToString() ?? "null").ToList();
                            writer.WriteLine(string.Join(",", propList)); 
                        }
                    }
                }
            }

        

        public static List<T> IncarcareDate<T>(string caleFisier, Func<T> fabricaDeObiecte) where T : class
        {
            var lista = new List<T>();

            if (!File.Exists(caleFisier))
                return lista;

            using (var reader = new StreamReader(caleFisier))
            {
                T? obiect = null;

                while (!reader.EndOfStream)
                {
                    var linie = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(linie))
                    {
                        if (obiect != null)
                        {
                            lista.Add(obiect);
                            obiect = null;
                        }
                        continue;
                    }

                    if (obiect == null)
                        obiect = fabricaDeObiecte();

                    var parti = linie.Split(": ", 2);
                    if (parti.Length == 2)
                    {
                        var propName = parti[0];
                        var propValue = parti[1];

                        var prop = typeof(T).GetProperty(propName);
                        if (prop != null)
                        {
                            var convertedValue = Convert.ChangeType(propValue, prop.PropertyType);
                            prop.SetValue(obiect, convertedValue);
                        }
                    }
                }

                if (obiect != null)
                    lista.Add(obiect);
            }

            return lista;
        }
        
    }

}