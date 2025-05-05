public abstract class TerrainBase
{
    public string Nom { get; set; }
    public float Humidite { get; set; } // 0.0 à 1.0
    public float Fertilite { get; set; } // 0.0 à 1.0
    public string TypeSol { get; set; } // "argile", "sable", etc.

    public abstract string ObtenirDescription();

    public virtual bool EstAdaptéPour(string typeSolPrefere)
    {
        return TypeSol.ToLower() == typeSolPrefere.ToLower();
    }
}