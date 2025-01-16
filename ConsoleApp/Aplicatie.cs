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
    private List<Tema> teme=new List<Tema>();
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
            Console.WriteLine("Apasă orice tastă pentru a iesi...");
            Console.ReadKey();
        }
        finally
        {
            SalveazaDate();
        }
    }

    //MeniuPrincipal
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

    
    //Meniu utilizator
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

    
    //MeniuStudent
    private void AfisareMeniuStudent(Student student)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Meniu Student: {student.Nume} ===");
            Console.WriteLine("1. Vizualizează teme");
            Console.WriteLine("2. Predă temă");
            Console.WriteLine("3. Vizualizează note");
            Console.WriteLine("0. Revenire la meniul principal");
            Console.Write("Alege o opțiune: ");

            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    StudentService.AfiseazaTeme(teme);
                    break;
                case "2":
                   StudentService.PredaTema(student,teme,rezolvari);
                    break;
                case "3":
                    StudentService.AfiseazaNote(teme,student.Nume); // Apelul funcției
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă. Încearcă din nou.");
                    break;
            }
        }
    }
    

    //Meniu Profesor
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
                   TemaService.CreeazaTema(teme,profesor);
                    break;
                case "2":
                   TemaService.EvalueazaRezolvare(rezolvari);
                    break;
                case "3":
                    TemaService.ArataStatisticiTeme(teme);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă. Încearcă din nou.");
                    break;
            }
        }  
    }
    
    //Meniu Administrator
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
                    Console.WriteLine("Introduceti username-ul utilizatorul");
                    string username = Console.ReadLine();
                     Admin.InrolareUtilizator(username,utilizatori);
                    break;
                case "2":
                     Console.WriteLine("Introduceti ID utilizatorul");
                     int id = int.Parse(Console.ReadLine());
                     Utilizator utilizatorselectat = null;
                     foreach (var utilizatordesters in utilizatori)
                     {
                         if (utilizatordesters.Id == id)
                         {
                             utilizatorselectat = utilizatordesters;
                             break;
                         }
                     }
                     Admin.StergereUtilizator(utilizatorselectat.Id);
                    break;
                case "3":
                    VizualizareUtilizatori(utilizatori);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă. Încearcă din nou.");
                    break;
            }
        }
    }

    public void VizualizareUtilizatori(List<Utilizator> utilizatori)
    {
        if (utilizatori.Count == 0)
        {
            Console.WriteLine("Nu există utilizatori înregistrati.");
        }
        else
        {
            Console.WriteLine("Lista de utilizatori:");
            foreach (var utilizator in utilizatori)
            {
                Console.WriteLine($"- Nume {utilizator.Nume} ,ID {utilizator.Id} ,Username {utilizator.Username}");
            }
        }
    }
}


