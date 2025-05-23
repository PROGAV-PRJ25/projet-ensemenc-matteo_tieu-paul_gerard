public abstract class PlanteNonComestible : Plante
{
    // Durée de vie maximale de la plante non comestible
    public int DureeVieMaximale { get; protected set; }

    // Nombre de tours écoulés depuis la plantation
    public int ToursEcoules { get; set; }

    // Constructeur de PlanteNonComestible
    public PlanteNonComestible(string nom, int dureePousseBase, int dureeVieMaximale, TypeTerrain typeTerrainAffinite) : base(nom, dureePousseBase, typeTerrainAffinite)
    {
        DureeVieMaximale = dureeVieMaximale;
        ToursEcoules = 0;
    }

    // Vérifie si la plante non comestible est morte de vieillesse
    public bool EstMorteDeVieillesse()
    {
        return ToursEcoules >= DureeVieMaximale;
    }
}
