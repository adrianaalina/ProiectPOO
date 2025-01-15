using Models;

namespace ConsoleApp.Services
{
    public class TemaService
    {
         public static void CreeazaTema(List<Tema> teme,Profesor profesor)
    {
        Console.Clear();
        Console.WriteLine("|*** Creeaza Tema ***|");
        Console.WriteLine("Introduceti titlul temei");
        string nume = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nume))
        {
            Console.WriteLine("Titlul temei nu poate fi gol. Apasa orice tasta pentru a reveni.");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Introduceti cerinta temei");
        string cerinta = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(cerinta))
        {
            Console.WriteLine("Cerinta temei nu poate fi aceasta. Apasa orice tasta pentru a reveni.");
            Console.ReadKey();
            return;
        }
        Console.Write("Introduceti data activării (formatul trebuie sa fie yyyy-MM-dd) ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataActivare))
        {
            Console.WriteLine("Data activării este invalidă. Apasă orice tastă pentru a reveni.");
            Console.ReadKey();
            return;
        }
        Console.Write("Introdu deadline-ul (formatul trebuie sa fie yyyy-MM-dd) ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
        {
            Console.WriteLine("Deadline-ul este invalid. Apasă orice tastă pentru a reveni.");
            Console.ReadKey();
            return;
        }
        else if (dataActivare >= deadline)
        {
            Console.WriteLine("Data activării trebuie să fie înainte de deadline. Apasă orice tastă pentru a reveni.");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Tema este obligatorie? Daca este apasati '1',daca nu este apasati '0'.");
        int x = int.Parse(Console.ReadLine());
        bool esteObligatorie;
        if (x == 0)
        {
            esteObligatorie = false;
        }
        else if (x == 1)
        {
            esteObligatorie = true;
        }
        else
        {
            Console.WriteLine("Aceasta varianta nu exista!");
            return;
        }
        Console.WriteLine("Tema poate fi predata cu intarziere? Daca este apasati '1',daca nu este apasati '0'.");
        int y = int.Parse(Console.ReadLine());
        bool predataintarziere;
        if (y == 0)
        {
            predataintarziere = false;
        }
        else if (y == 1)
        {
            predataintarziere = true;
        }
        else
        {
            Console.WriteLine("Aceasta varianta nu exista!");
            return;
        }

        StatutTema statut = StatutTema.Nepredat;
        var tema = new Tema(
            id: teme.Count + 1,
            nume: nume,
            cerinta: cerinta,
            dataActivare: dataActivare,
            deadline: deadline,
            esteObligatorie: esteObligatorie,
            permitePredareIntarziata: predataintarziere,
            statut:statut
        );
        teme.Add(tema);
        Console.WriteLine("Tema a fost creata!");
        Console.WriteLine("Apasă orice tastă pentru a reveni la meniu.");
        Console.ReadKey();
        
    }

    public static void EvalueazaRezolvare(List<Rezolvare> rezolvari)
    {
        double nota;
        Console.WriteLine("Rezolvările disponibile:");
        foreach (var rezolvare in rezolvari)
        {
            Console.WriteLine($"ID: {rezolvare.Id}, Student ID: {rezolvare.StudentId}");
        }
        Console.Write("Introduceți ID-ul rezolvării pe care doriți să o evaluați: ");
        int idRezolvare = int.Parse(Console.ReadLine());

        Rezolvare rezolvareSelectata = rezolvari.FirstOrDefault(r => r.Id == idRezolvare);

        if (rezolvareSelectata != null)
        {
            Console.WriteLine($"Rezolvarea selectată: Tema: {rezolvareSelectata.TemaNume}, Student ID: {rezolvareSelectata.StudentId}");
            Console.WriteLine($"Răspuns: {rezolvareSelectata.Continut}");
            Console.Write("Introduceți nota: ");
            double.TryParse(Console.ReadLine(),out nota);
            Console.WriteLine($"Nota acordată: {rezolvareSelectata.Nota}");
        }
        else
        {
            Console.WriteLine("Rezolvarea cu acest ID nu a fost găsită.");
        } 
      
    }
    
    public static void ArataStatisticiTeme(List<Tema> teme)
    {
        
        var statistici = teme.GroupBy(t => t.Statut)
            .Select(g => new
            {
                Statut = g.Key,
                Count = g.Count()
            }).ToList();

        if (statistici.Count == 0)
        {
            Console.WriteLine("Nu există teme înregistrate.");
            return;
        }
        
        Console.WriteLine("Statistici teme:");

        foreach (var statistica in statistici)
        {
            Console.WriteLine($"{statistica.Statut}: {statistica.Count} teme");
        }
    }
    
}
}


