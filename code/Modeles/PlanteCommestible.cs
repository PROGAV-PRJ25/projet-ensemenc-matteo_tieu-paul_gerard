
public abstract class PlanteComestible : Plante
{
    public int DureeVieApresMaturation { get; protected set; } // Durée de vie après la maturité
    public int ToursDepuisMaturation { get; set; } // Nombre de tours depuis que la plante a atteint sa maturité

    // Constructeur de PlanteComestible
    public PlanteComestible(string nom, int dureePousseBase, int dureeVieApresMaturation, TypeTerrain typeTerrainAffinite)
        : base(nom, dureePousseBase, typeTerrainAffinite)
    {
        DureeVieApresMaturation = dureeVieApresMaturation;
        ToursDepuisMaturation = 0;
    }

    // Surcharge de la méthode EstMature pour les plantes comestibles
    public override bool EstMature()
    {
        // Une plante comestible est mature quand ses tours restants avant maturité sont à 0
        return base.EstMature();
    }

    // Méthode pour vérifier si la plante est morte de vieillesse
    public bool EstMorteDeVieillesse()
    {
        return EstMature() && ToursDepuisMaturation >= DureeVieApresMaturation;
    }
}
