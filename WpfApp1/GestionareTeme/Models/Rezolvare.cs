namespace WpfApp1.GestionareTeme.Models
{

    public class Rezolvare
    {
        public int Id { get; set; }
        public int TemaId {get; set;}
        public int StudentId { get; set; }
        public string Raspuns{ get; set; }
        public List<string> FisiereIncarcate { get; set; }
        public DateTime DataPredare  { get; set; }
        public bool EsteEvaluata { get; set; }
        public int Nota { get; set; }
        public string Comentarii { get; set; }

        public Rezolvare(int id, int temeId, int studentId, string raspuns, List<string> fisiereIncarcate,
            DateTime dataPredare)
        {
            Id = id;
            TemaId = temeId;
            StudentId = studentId;
            Raspuns = raspuns;
            FisiereIncarcate = fisiereIncarcate ?? new List<string>();
            DataPredare = dataPredare;
            EsteEvaluata = false;
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