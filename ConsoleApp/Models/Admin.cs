
namespace Models
{

    public class Admin : Utilizator
    {
        public Admin(int id, string nume, string username, string parola) : base(id, nume, username, parola)
        {
        }

        public static void InrolareUtilizator(string username,List<Utilizator>utilizatori)
        {
            
            if (UtilizatorExistent(username, utilizatori))
            {
                Console.WriteLine("Acest nume de utilizator este deja folosit. Alegeți altul.");
                return;
            }
            Console.WriteLine("Introduceți parola: ");
            string parola = Console.ReadLine();
            int idnou=utilizatori.Count+1;
            Console.WriteLine("Introduceți numele dumneavoastra: ");
            string numeutnou = Console.ReadLine();
            Utilizator utilizatorNou = new Utilizator(idnou,numeutnou,username, parola);
            utilizatori.Add(utilizatorNou);
            Console.WriteLine($"Utilizatorul {utilizatorNou.Id} - {utilizatorNou.Nume} a fost inrolat!");
        }


        public static void StergereUtilizator(int userId)
        {
            Console.WriteLine($"Utilizatorul cu ID-ul {userId} a fost sters!");
        }

        public static bool UtilizatorExistent(string nume, List<Utilizator> utilizatori)
        {
            foreach (var utilizator in utilizatori)
            {
                if (utilizator.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

            }

            return false;
        }
    }
        
}