using ProiectPOO.Models;

namespace ProiectPOO.Models;

public class Student: Utilizator
{
    public List<int> RezolvariTemaIds { get; set; }

    public Student(int id, string nume, string username, string parola) : base(id, nume, username, parola)
    {
        RezolvariTemaIds = new List<int>();
    }

    public override void AfisareDetalii()
    {
        base.AfisareDetalii();
        Console.WriteLine($"Numar de teme rezolvate: {RezolvariTemaIds.Count}");
    }
}