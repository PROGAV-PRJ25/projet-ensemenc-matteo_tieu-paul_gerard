public class Lavande : PlanteNonComestible
{
    // Constructeur de Lavande
    public Lavande()
        : base("Lavande", 6, 15, TypeTerrain.Sableux) // Nom, durée de pousse, durée de vie maximale, affinité terrain
    {
    }

    // Retourne l'icône spécifique à la lavande
    public override string ObtenirIcone()
    {
        // La lavande a une icône spécifique une fois mature
        if (EstMature())
        {
            return "💜";
        }
        return "🌱"; // Icône semis/croissance
    }
}
