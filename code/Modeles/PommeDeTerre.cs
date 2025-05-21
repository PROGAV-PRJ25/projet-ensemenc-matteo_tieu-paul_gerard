public class PommeDeTerre : PlanteComestible
{
    // Constructeur de PommeDeTerre
    public PommeDeTerre()
        : base("Pomme de terre", 6, 12, TypeTerrain.Argileux) // Nom, durée de pousse, durée de vie après maturation, affinité terrain
    {
    }

    // Retourne l'icône spécifique à la pomme de terre mature
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🥔"; // Icône pomme de terre mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
