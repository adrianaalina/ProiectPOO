namespace Models
{

    public class Rezolvare
    {
        public int Id { get; set; }
        public int TemaId {get; set;}
        public string TemaNume {get; set;}
        public int StudentId { get; set; }
        public string Continut{ get; set; }
        public List<string> FisiereIncarcate { get; set; }
        public DateTime DataPredare  { get; set; }
        public bool EsteEvaluata { get; set; }
        public double Nota { get; set; }
        public string Comentarii { get; set; }

        public Rezolvare(int id, int temeId,string temaNume, int studentId, string continut, List<string> fisiereIncarcate,
            bool esteEvaluata,DateTime dataPredare)
        {
            Id = id;
            TemaId = temeId;
            TemaNume = temaNume;
            StudentId = studentId;
            Continut = continut;
            FisiereIncarcate = fisiereIncarcate ?? new List<string>();
            DataPredare = dataPredare;
            EsteEvaluata = esteEvaluata;
            Nota = 0;
            Comentarii = string.Empty;
        }

        public void Evaluaza(int nota, string comentarii)
        {
            EsteEvaluata = true;
            Nota = nota;
            Comentarii = comentarii;
        }
      
    }
}