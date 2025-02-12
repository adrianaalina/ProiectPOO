﻿namespace Models
{

    public enum StatutTema
    {
        Predat,
        Nepredat,
        Evaluat,
        Neevaluat
    }
    public class Tema
    {
     public int Id { get; set; }
     public string Nume { get; set; }
     public string Student { get; set; }
     public string Cerinta { get; set; }
     public DateTime DataActivare { get; set; }
     public DateTime DeadLine { get; set; }
     public bool EsteObligatorie { get; set; }
     public bool PermitePredareIntarziata{ get; set; }
     public double Nota { get; set; } 
     public StatutTema Statut { get; set; }


     public Tema(int id, string nume, string cerinta, DateTime dataActivare, DateTime deadline, bool esteObligatorie, bool permitePredareIntarziata,StatutTema statut)
     {
         Id = id;
         Nume = nume;
         Cerinta = cerinta;
         DataActivare = dataActivare;
         DeadLine = deadline;
         EsteObligatorie = esteObligatorie;
         PermitePredareIntarziata = permitePredareIntarziata;
         Statut = statut;

     }

     public virtual void AfiseazaDetalii()
     {
         Console.WriteLine($"Tema: {Nume}, Deadline{DeadLine.ToShortDateString()}, Obligatorie{EsteObligatorie}, Nota{Nota}");
     }
    }
}