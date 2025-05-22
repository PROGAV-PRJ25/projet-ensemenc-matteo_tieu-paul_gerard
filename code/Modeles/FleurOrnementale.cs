public class FleurOrnementale : PlanteNonComestible
{
    // Constructeur de FleurOrnementale
    public FleurOrnementale() : base("Fleur Ornementale", 3, 5, TypeTerrain.Sableux) { }
    // Nom, durée de pousse, durée de vie maximale, affinité terrain

    // Retourne l'icône représentant la fleur ornementale
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🌸"; // Icône fleur ornementale mature
        }
        return "🌱"; // Icône semis/croissance
    }
}