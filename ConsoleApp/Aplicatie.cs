namespace ProiectPOO;
using ProiectPOO.Models;
using ProiectPOO.Utilities;


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
            utilizatori = FileHelper.IncarcareDate<Utilizator>("Data/Files/utilizatori.json");
            teme = FileHelper.IncarcareDate<Tema>("Data/Files/teme.json");
            rezolvari = FileHelper.IncarcareDate<Rezolvare>("Data/Files/rezolvari.json");

            Console.WriteLine("Datele au fost încărcate cu succes!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la încărcarea datelor: {ex.Message}");
            utilizatori = new List<Utilizator>();
            teme = new List<Tema>();
            rezolvari = new List<Rezolvare>();
        }
    }

    private void SalveazaDate()
    {
        try
        {
            FileHelper.SalvareDate("Data/Files/utilizatori.json", utilizatori);
            FileHelper.SalvareDate("Data/Files/teme.json", teme);
            FileHelper.SalvareDate("Data/Files/rezolvari.json", rezolvari);

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
            // Aici vine logica aplicației: autentificare, meniuri, funcționalități
            Console.WriteLine("Aplicația rulează...");
            Console.WriteLine("Apasă orice tastă pentru a ieși...");
            Console.ReadKey();
        }
        finally
        {
            SalveazaDate();
        }
    }
}

