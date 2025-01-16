namespace Models
{

    public class Quiz: Tema
    { public static List<string> Intrebari { get; set; }

        public Quiz(int id, string nume, string cerinta, DateTime dataActivare, DateTime deadline, bool esteObligatorie,
            bool predarecuintarziere, StatutTema statut) : base(id, nume, cerinta, dataActivare, deadline,
            esteObligatorie, predarecuintarziere, statut)
        {
            Intrebari = new List<string>();
        }
        public int TimpLimita{ get; set; }

        public static void AdaugareIntrebari(string intrebare)
        {
            Intrebari.Add(intrebare);
        }

        public void SeteazaTimpLimita()
        {
            Console.WriteLine("Introduceti timpul limita pentru quiz (in minute)");
            while (true)
                try
                {
                    TimpLimita = int.Parse(Console.ReadLine());
                    if (TimpLimita > 0)
                    {
                        Console.WriteLine($"Timpul limita a fost setat la {TimpLimita} minute");
                        break;
                    }
                    else
                    {
                            Console.WriteLine("Timpul trebuie sa fie un numar pozitiv!"); 
                    }
                }
                catch
                {
                    Console.WriteLine("Introduceti un numar valid");
                }   
            
        }

        public override void AfiseazaDetalii()
        {
            base.AfiseazaDetalii();
            Console.WriteLine($"Numar de intrebari:{Intrebari.Count}");
        }

    }
}