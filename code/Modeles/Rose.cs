public class Rose : PlanteNonComestible
{
    // Constructeur de Rose
    public Rose() : base("Rose", 7, 18, TypeTerrain.Terre) { }
    // Nom, durée de pousse, durée de vie maximale, affinité terrain

    // Retourne l'icône représentant la rose
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🌹"; // Icône rose mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
