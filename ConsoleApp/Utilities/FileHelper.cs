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
                        // Verifică dacă obiectul este de tip Utilizator
                        if (item is Utilizator utilizator)
                        {
                            string tipUtilizator = "student"; // Default

                            // Logica de determinare a tipului
                            if (utilizator.Username.Contains("admin"))
                            {
                                tipUtilizator = "admin";  // Pentru utilizatori de tip admin
                            }
                            else if (utilizator.Username.Contains("prof"))
                            {
                                tipUtilizator = "profesor"; // Pentru utilizatori de tip profesor
                            }
                            // "student" rămâne implicit pentru toți ceilalți

                            // Scrie datele utilizatorului, adăugând tipul (fără a modifica clasa Utilizator)
                            writer.WriteLine($"{utilizator.Id},{utilizator.Nume},{utilizator.Username},{utilizator.Parola},{tipUtilizator}");
                        }
                        else
                        {
                            // Dacă este alt tip de obiect, salvează într-un mod generic
                            var properties = typeof(T).GetProperties();
                            var propList = properties.Select(p => p.GetValue(item)?.ToString() ?? "null").ToList();
                            writer.WriteLine(string.Join(",", propList)); // Salvează datele separate prin virgulă
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