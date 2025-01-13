
namespace GestionareTeme.Models
{

    public class Admin : Utilizator
    {
        public Admin(int id, string nume, string username, string parola) : base(id, nume, username, parola)
        {
        }

        public void InrolareUtilizator(Utilizator utilizator)
        {
            Console.WriteLine($"Utilizatorul {utilizator.Id} - {utilizator.Nume} a fost inrolat!");
        }


        public void StergereUtilizator(int userId)
        {
            Console.WriteLine($"Utilizatorul cu ID-ul {userId} a fost sters!");
        }
    }
}