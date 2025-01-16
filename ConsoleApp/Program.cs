// See https://aka.ms/new-console-template for more information

using Aplicatie;

class Program
{
    static void Main(string[] args)
    {
        var aplicatie = new Aplicatie.Aplicatie();
        aplicatie.Start();
        aplicatie.AfisareMeniuPrincipal();
    }
    
}