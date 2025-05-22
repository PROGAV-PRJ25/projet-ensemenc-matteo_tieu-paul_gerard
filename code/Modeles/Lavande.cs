public class Lavande : PlanteNonComestible
{
    // Constructeur de Lavande
    public Lavande() : base("Lavande", 6, 15, TypeTerrain.Sableux) { }
    // Nom, durée de pousse, durée de vie maximale, affinité terrain

    // Retourne l'icône représentant la lavande
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "💜"; // Icône lavande mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
