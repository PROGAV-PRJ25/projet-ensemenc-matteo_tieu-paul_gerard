public class Rose : PlanteNonComestible
{
    // Constructeur de Rose
    public Rose()
        : base("Rose", 7, 18, TypeTerrain.Terre) // Nom, durée de pousse, durée de vie maximale, affinité terrain
    {
    }

    // Retourne l'icône spécifique à la rose
    public override string ObtenirIcone()
    {
        // La rose a une icône spécifique une fois mature
        if (EstMature())
        {
            return "🌹";
        }
        return "🌱"; // Icône semis/croissance
    }
}
