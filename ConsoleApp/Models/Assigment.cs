using Models;
namespace DefaultNamespace;

public class Assigmment : Rezolvare
{
    private string continutAssigmn{get;set;}
    public Assigmment (string continutAssigmn,int id, int temeId,string temaNume, int studentId, string continut, List<string> fisiereIncarcate,
        bool esteEvaluata,DateTime dataPredare):base(id,  temeId, temaNume, studentId, continut, fisiereIncarcate,
        esteEvaluata,dataPredare)
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