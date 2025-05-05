public abstract class Plante
{
    public string Nom { get; set; }
    public double Santé { get; set; } = 100;
    public double Taille { get; set; } = 0;
    public int DureeDeCroissance { get; set; }
    public int DureeDeVie { get; set; }
    public int BesoinEau { get; set; }
    public int BesoinLuminosite { get; set; }
    public float TempMin, TempMax;
    public List<string> Maladies { get; set; }

    public abstract void Croître();
    public abstract void VérifierSanté();
    public abstract void SubirUrgence();
    public abstract bool EstMûre();

}