using System.Buffers.Text;
using System.Text;
using System.Text.Unicode;
using ConsoleApp.Services;


namespace Aplicatie;
using Models;
using Utilities;


public class Aplicatie
{
    private List<Utilizator> utilizatori;
    private List<Tema> teme;
    private List<Rezolvare> rezolvari;
    private TemaService temaService = new TemaService();
    public Aplicatie()
    {
        utilizatori = new List<Utilizator>();
        teme = new List<Tema>();
        rezolvari = new List<Rezolvare>();

        IncarcaDateInitiale();
    }


    private void IncarcaDateInitiale()
    {
        try
        {
           utilizatori = Utilities.FileHelper.FileHelper.IncarcareDate<Utilizator>("Data/Files/utilizatori.json" );
            teme = Utilities.FileHelper.FileHelper.IncarcareDate<Tema>("Data/Files/teme.json");
            rezolvari = Utilities.FileHelper.FileHelper.IncarcareDate<Rezolvare>("Data/Files/rezolvari.json");

            Console.WriteLine("Datele au fost incarcate cu succes!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la incarcarea datelor: {ex.Message}");
            utilizatori = new List<Utilizator>();
            teme = new List<Tema>();
            rezolvari = new List<Rezolvare>();
        }
    }

    private void SalveazaDate()
    {
        try
        { 
            Utilities.FileHelper.FileHelper.SalvareDate("Data/Files/utilizatori.json", utilizatori);
            Utilities.FileHelper.FileHelper.SalvareDate("Data/Files/teme.json", teme);
            Utilities.FileHelper.FileHelper.SalvareDate("Data/Files/rezolvari.json", rezolvari);

            Console.WriteLine("Datele au fost salvate cu succes!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la salvarea datelor: {ex.Message}");
        }
    }



    public void Start()
    {
        
        try
        {
            Console.WriteLine("Bine ai venit la Sistemul de Gestionare a Temelor!");
            AfisareMeniuPrincipal();
            
            // Aici vine logica aplicației: autentificare, meniuri, funcționalități
            Console.WriteLine("Apasă orice tastă pentru a iesi...");
            Console.ReadKey();
        }
        finally
        {
            SalveazaDate();
        }
    }

    public void AfisareMeniuPrincipal()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("***  Aplicatia de gestionare a temelor  ***");
            Console.WriteLine("Pentru autentificare tastati '1', iar pentru iesire '0'");
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("(◔_◔)?");  // de verificat cum se foloseste UTF-8
            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1": 
                    AutentificareUtilizator();
                    break;
                case "0":
                    Console.WriteLine("La revedere! (^-^)");
                    return;
                default:
                    Console.WriteLine("Optiune invalida! :("); //emoticon nou
                    break;
            }
        }
    }

    private void AutentificareUtilizator()
    {
        Console.Clear();
        Console.WriteLine("|^^^   Autentificare ca utilizator  ^^^|");
        Console.WriteLine();
        Console.WriteLine("*** Introduceti numele de utilizator *** ");
        string username = Console.ReadLine();
        Console.WriteLine("*** Introduceti parola ***");
        string parola = Console.ReadLine();
        
        var utilizator=utilizatori.FirstOrDefault(u => u.Nume == username && u.Parola == parola);

        if (utilizator == null)
        {
            Console.WriteLine("Utilizatorul nu a fost gasi! :("); // de cautat emoticoane noi
            Console.WriteLine("Apasa o tasta prntru a reveni la meniu"); 
            Console.ReadKey();
            return;
        }
        else
        {
            Console.WriteLine($"Bine ai venit, {utilizator.Nume}");
            if(utilizator is Student student)
            {
             AfisareMeniuStudent(student);   
            }
            else if (utilizator is Profesor profesor)
            {
                AfisareMeniuProfesor(profesor);
            }
            else if (utilizator is Admin admin)
            {
                AfisareMeniuAdmin(admin);
            }
        }

    }

    private void AfisareMeniuStudent(Student student)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Meniu Student: {student.Nume} ===");
            Console.WriteLine("1. Vizualizează teme");
            Console.WriteLine("2. Predă temă");
            Console.WriteLine("0. Revenire la meniul principal");
            Console.Write("Alege o opțiune: ");

            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    VizualizeazaTeme();
                    break;
                case "2":
                   PredaTema(student,teme,rezolvari);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă. Încearcă din nou.");
                    break;
            }
        }
    }

    public void PredaTema(Student student, List<Tema> teme, List<Rezolvare> rezolvari)
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
                Console.WriteLine($"ID: {tema.Id}, Nume: {tema.Nume}, Deadline: {tema.DeadLine.ToShortDateString()}");
            }

            Console.Write("\nIntrodu ID-ul temei pentru care dorești să predai soluția: ");
            if (!int.TryParse(Console.ReadLine(), out int temaId))
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


    private void VizualizeazaTeme()
    {
        temaService.AfiseazaTeme(teme);
    }

    private void AfisareMeniuProfesor(Profesor profesor)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Meniu Profesor: {profesor.Nume} ===");
            Console.WriteLine("1. Creează temă");
            Console.WriteLine("2. Evaluează teme");
            Console.WriteLine("3. Vizualizează statistici");
            Console.WriteLine("0. Revenire la meniul principal");
            Console.Write("Alege o opțiune: ");

            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                   // CreeazaTema(profesor);
                    break;
                case "2":
                   // EvalueazaTeme(profesor);
                    break;
                case "3":
                    //VizualizeazaStatistici();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă. Încearcă din nou.");
                    break;
            }
        }  
    }


    private void AfisareMeniuAdmin(Admin admin)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Meniu Admin: {admin.Nume} ===");
            Console.WriteLine("1. Adaugă utilizator");
            Console.WriteLine("2. Șterge utilizator");
            Console.WriteLine("3. Vizualizează utilizatori");
            Console.WriteLine("0. Revenire la meniul principal");
            Console.Write("Alege o opțiune: ");

            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    //AdaugaUtilizator();
                    break;
                case "2":
                    //StergeUtilizator();
                    break;
                case "3":
                   // VizualizeazaUtilizatori();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă. Încearcă din nou.");
                    break;
            }
        }
    }
    
}


