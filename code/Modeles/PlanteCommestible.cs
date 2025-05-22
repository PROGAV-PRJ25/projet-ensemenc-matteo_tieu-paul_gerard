
public abstract class PlanteComestible : Plante
{
    // Durée de vie après la maturité
    public int DureeVieApresMaturation { get; protected set; }

    // Nombre de tours depuis que la plante a atteint sa maturité
    public int ToursDepuisMaturation { get; set; }

    // Constructeur de PlanteComestible
    public PlanteComestible(string nom, int dureePousseBase, int dureeVieApresMaturation, TypeTerrain typeTerrainAffinite)
        : base(nom, dureePousseBase, typeTerrainAffinite)
    {
        DureeVieApresMaturation = dureeVieApresMaturation;
        ToursDepuisMaturation = 0;
    }

    // Vérifie si la plante est morte de vieillesse
    public bool EstMorteDeVieillesse()
    {
        return EstMature() && ToursDepuisMaturation >= DureeVieApresMaturation;
    }
}
