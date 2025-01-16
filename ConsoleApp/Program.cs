// See https://aka.ms/new-console-template for more information

using Aplicatie;
using Models;
using Utilities.FileHelper;

class Program
{
    static void Main(string[] args)
    {
        var studenti = new List<Student>();
        var student1 = new Student( 1, "Ion",  "ion",  "parola");
        var student2 =   new Student (  2,  "Maria",  "maria", "parola" );
        studenti.Add(student1);
        studenti.Add(student2);

        FileHelper.SalvareDate("studenti.txt", studenti);
        var aplicatie = new Aplicatie.Aplicatie();
        aplicatie.Start();
        aplicatie.AfisareMeniuPrincipal();
        
    }
    
}