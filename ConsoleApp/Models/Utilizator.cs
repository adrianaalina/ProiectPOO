namespace Models;

public class Utilizator
{
    public int Id { get; set; }
    public string Nume { get; set; }
    public string Username { get; set; }
    public string Parola { get; set; }

    public Utilizator(int id, string nume, string usernam, string parola)
    {
        Id = id;
        Nume = nume;
        Username = usernam;
        Parola = parola;
    }

    public virtual void AfisareDetalii()
    {
        Console.WriteLine($"ID:{Id},Nume:{Nume},Username:{Username}");
    }
}