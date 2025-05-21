
// Classe Tournesol dérivée de PlanteNonComestible
public class Tournesol : PlanteNonComestible
{
    // Constructeur de Tournesol
    public Tournesol()
        : base("Tournesol", 5, 10, TypeTerrain.Argileux) // Nom, durée de pousse, durée de vie maximale, affinité terrain
    {
    }

    // Retourne l'icône spécifique au tournesol
    public override string ObtenirIcone()
    {
        // Le tournesol a une icône spécifique une fois mature
        if (EstMature())
        {
            return "🌻";
        }
        return "🌱"; // Icône semis/croissance
    }
}
