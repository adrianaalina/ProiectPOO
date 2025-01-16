using System.Buffers.Text;
using System.Data;
using System.Text;
using System.Text.Unicode;
using ConsoleApp.Services;
using System.Collections.Generic;


namespace Aplicatie;
using Models;
using Utilities;


public class Aplicatie
{
    private List<Utilizator> utilizatori;
    private List<Student> studenti;
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
            DateTime dataInitiala = new DateTime(2015, 12, 23);
            List<string> fisiere = new List<string>();
            var utilizatori = Utilities.FileHelper.FileHelper.IncarcareDate<Utilizator>("utilizatori.txt",()=>new Utilizator(0, "", "", "") );
           var teme = Utilities.FileHelper.FileHelper.IncarcareDate<Tema>("teme.txt", () => new Tema(0,"","",dataInitiala,dataInitiala,false,true,StatutTema.Nepredat));
            rezolvari = Utilities.FileHelper.FileHelper.IncarcareDate<Rezolvare>("rezolvari.txt", () => new Rezolvare(0,0,"",0,"",fisiere,false,dataInitiala));

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
        string filePath = "utilizatori.txt";
        var linii = File.ReadAllLines(filePath);
        var linieUtilizator = linii.FirstOrDefault(l => l.StartsWith(username + ","));
            
            bool autentificat = VerificaLogare(username, parola);

            if (autentificat)
            {
                Console.WriteLine("Logare reușită!");
            }
            else
            {
                Console.WriteLine("Logare eșuată. Verificați username-ul sau parola.");
            }

        // Functie pentru verificarea logării
        static bool VerificaLogare(string username, string parola)
        {
            string fisierUtilizatori = "utilizatori.txt";

            if (!File.Exists(fisierUtilizatori))
            {
                Console.WriteLine("Fișierul utilizatori.txt nu există.");
                return false;
            }

            // Citire linii din fișier
            string[] linii = File.ReadAllLines(fisierUtilizatori);

            foreach (string linie in linii)
            {
                string[] date = linie.Split(',');

                if (date.Length == 5)
                {
                    string usernameFisier = date[3];
                    string parolaFisier = date[4];
                    //de egalat tip cu date[0]

                    if (username == usernameFisier && parola == parolaFisier)
                    {
                        if($"type" is  student)
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
                    else
                    {
                        Console.WriteLine("Utilizatorul nu a fost gasit. Apasati orice tasta pentru a reveni.");
                        Console.ReadKey();
                        return;
                    }
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
                case "4":
                    var FormulaMedie = TemaService.FormulaMedie();
                    TemaService.CalculeazaMedii(rezolvari,studenti, FormulaMedie); 
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
            Console.WriteLine("4. Creeaza tema");
            Console.WriteLine("5. Vizualizeaza statistici");
            Console.WriteLine("6. Evalueaza teme");
            Console.WriteLine("7. Vizualizeaza teme");
            Console.WriteLine("8. Preda teme");
            Console.WriteLine("9. Vizualizeaza note");
            Console.WriteLine("0. Revenire la meniul principal");
            Console.Write("Alege o opțiune: ");

            string optiune = Console.ReadLine();
            int idNewP = 0;
            string numeNewP = "";
            string materialeNewP = "";
            string parolaNewP = "";
            
            int idNewS = 0;
            string numeNewS = "";
            string materialeNewS = "";
            string parolaNewS = "";
            
            Profesor profesorNew=new Profesor(idNewP,numeNewP,materialeNewP,parolaNewP);
            Student studentNew=new Student(idNewS,numeNewS,materialeNewS,parolaNewS);
            
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
                case "4":
                    TemaService.CreeazaTema(teme,profesorNew);
                    break;
                case "5":
                    TemaService.ArataStatisticiTeme(teme);
                    break;
                case "6":
                    TemaService.EvalueazaRezolvare(rezolvari);
                    break;
                case "7":
                    StudentService.AfiseazaTeme(teme);
                    break;
                case "8": 
                    StudentService.PredaTema(studentNew,teme,rezolvari);
                    break;
                case "9":   
                    StudentService.AfiseazaNote(teme,studentNew.Nume);
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


