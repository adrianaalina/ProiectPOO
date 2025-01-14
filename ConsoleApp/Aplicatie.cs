using System.Buffers.Text;
using System.Text;
using System.Text.Unicode;


namespace Aplicatie;
using Models;
using Utilities;


public class Aplicatie
{
    private List<Utilizator> utilizatori;
    private List<Tema> teme;
    private List<Rezolvare> rezolvari;

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
             //AfisareMeniuStudent();   
            }
            else if (utilizator is Profesor profesor)
            {
                //AfisareMeniuProfesor();
            }
            else if (utilizator is Admin admin)
            {
                //AfisareMeniuAdmin();
            }
        }

    }
}


