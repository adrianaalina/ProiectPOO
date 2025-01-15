using Models;

namespace ConsoleApp.Services
{

    public class StudentService
    {
        public static void AfiseazaTeme(List<Tema> teme)
        {
            if (teme == null || teme.Count == 0)
            {
                Console.WriteLine("Nu există teme disponibile.");
                return;
            }

            Console.WriteLine("\nTeme disponibile:");
            foreach (var tema in teme)
            {
                Console.WriteLine($"ID: {tema.Id}, Nume: {tema.Nume}, Deadline: {tema.DeadLine.ToShortDateString()}");
            }
        }

        public static void PredaTema(Student student, List<Tema> teme, List<Rezolvare> rezolvari)
        {
            Console.Clear();
            Console.WriteLine("=== Predare Temă ===");

            if (teme == null || teme.Count == 0)
            {
                Console.WriteLine("Nu există teme disponibile pentru predare.");
                Console.WriteLine("Apasă orice tastă pentru a reveni.");
                Console.ReadKey();
                return;
            }

            else
            {
                Console.WriteLine("\nTeme disponibile pentru predare:");
                foreach (var tema in teme)
                {
                    Console.WriteLine(
                        $"ID: {tema.Id}, Nume: {tema.Nume}, Deadline: {tema.DeadLine.ToShortDateString()}");
                }

                Console.Write("\nIntrodu ID-ul temei pentru care dorești să predai soluția: ");
                if (!int.TryParse(Console.ReadLine(), out int temaId))
                {
                    Console.WriteLine("ID invalid. Apasă orice tastă pentru a reveni.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("\nIntrodu numele temei pentru care dorești să predai soluția: ");
                string temanume = Console.ReadLine();
                if (!string.IsNullOrEmpty(temanume))
                {
                    Console.WriteLine("ID invalid. Apasă orice tastă pentru a reveni.");
                    Console.ReadKey();
                    return;
                }

                var temaSelectata = teme.FirstOrDefault(t => t.Id == temaId);
                if (temaSelectata == null)
                {
                    Console.WriteLine("Tema selectată nu există. Apasă orice tastă pentru a reveni.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.Write("Introdu fișierele încărcate (separate prin virgulă): ");
                    string fisiereInput = Console.ReadLine();
                    List<string> fisiereIncarcate = fisiereInput
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(f => f.Trim())
                        .ToList();
                    Console.Write("Introdu soluția ta: ");
                    string continut = Console.ReadLine();


                    var rezolvare = new Rezolvare(
                        id: rezolvari.Count + 1,
                        temeId: temaSelectata.Id,
                        temaNume: temanume,
                        studentId: student.Id,
                        fisiereIncarcate: fisiereIncarcate,
                        continut: continut,
                        dataPredare: DateTime.Now,
                        esteEvaluata: false
                    );


                    rezolvari.Add(rezolvare);

                    Console.WriteLine("Tema a fost predată cu succes!");
                    Console.WriteLine("Apasă orice tastă pentru a continua.");
                    Console.ReadKey();
                }
            }
        }
    }
}