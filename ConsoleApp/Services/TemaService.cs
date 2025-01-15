using Models;

namespace ConsoleApp.Services
{
    public class TemaService
    {
        public void AfiseazaTeme(List<Tema> teme)
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
    }
}
