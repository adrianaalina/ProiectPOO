namespace GestionareTeme
{

public class Aplicatie
{
    private void IncarcaDateInitiale()
    {
        utilizatori = FileHelper.IncarcaDate<Utilizator>("utilizatori.json");
        teme = FileHelper.IncarcaDate<Tema>("teme.json");
        rezolvari = FileHelper.IncarcaDate<Rezolvare>("rezolvari.json");
    }

    private void SalveazaDate()
    {
        FileHelper.SalvareDate("utilizatori.json", utilizatori);
        FileHelper.SalvareDate("teme.json", teme);
        FileHelper.SalvareDate("rezolvari.json", rezolvari);
    }

    
}
}