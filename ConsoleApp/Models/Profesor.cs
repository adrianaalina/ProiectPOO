namespace Models
{

    public class Profesor : Utilizator
    {
        public List<int> TemeGestionateIds { get; set; }

        public Profesor(int id, string nume, string username, string parola) : base(id, nume, username, parola)
        {
            TemeGestionateIds = new List<int>();
        }
        
        public override void AfisareDetalii()
        {
            base.AfisareDetalii();
            Console.WriteLine($"Numar de teme gestionate: {TemeGestionateIds.Count}");
        }
        

         

    }
}