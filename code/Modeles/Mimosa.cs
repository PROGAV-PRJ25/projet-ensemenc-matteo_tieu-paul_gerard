public class Mimosa : PlanteNonComestible
{
    // Constructeur de Mimosa
    public Mimosa() : base("Mimosa", 4, 12, TypeTerrain.Sableux) { }
    // Nom, durée de pousse, durée de vie maximale, affinité terrain

    // Retourne l'icône représentant le mimosa
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🌺"; // Icône mimosa mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
