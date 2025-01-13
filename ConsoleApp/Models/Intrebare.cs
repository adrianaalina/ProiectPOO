namespace ProiectPOO.Models
{

    public class Intrebare
    {
        public string Text { get; set; }
        public List<string> Optiuni { get; set; }
        public int IndiceRaspunsCorect {get; set;}

        public Intrebare(string text, List<string> optiuni, int indiceRaspunsCorect)
        {
            Text = text;
            Optiuni = optiuni ?? new List<string>();
            IndiceRaspunsCorect = indiceRaspunsCorect;
        }

        public bool VerificareRaspuns(int indiceRaspuns)
        {
            return indiceRaspuns==IndiceRaspunsCorect;
        }

    }
}