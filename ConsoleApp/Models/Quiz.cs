namespace Models
{

    public class Quiz: Tema
    { public List<string> Intrebari { get; set; }

        public Quiz(int id, string nume, string cerinta, DateTime dataActivare, DateTime deadline, bool esteObligatorie, bool predarecuintarziere,StatutTema statut)
            : base(id, nume, cerinta, dataActivare, deadline, esteObligatorie, predarecuintarziere,statut)
        {
            Intrebari = new List<string>();
        }

        public void AdaugareIntrebari(string intrebare)
        {
            Intrebari.Add(intrebare);
        }

        public override void AfiseazaDetalii()
        {
            base.AfiseazaDetalii();
            Console.WriteLine($"Numar de intrebari:{Intrebari.Count}");
        }

    }
}