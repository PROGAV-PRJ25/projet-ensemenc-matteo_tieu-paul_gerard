public class Tournesol : PlanteNonComestible
{
    // Constructeur de Tournesol
    public Tournesol()
        : base("Tournesol", 5, 10, TypeTerrain.Argileux) // Nom, durée de pousse, durée de vie maximale, affinité terrain
    {
    }

    // Retourne l'icône représentant le tournesol
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🌻"; // Icône tournesol mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
