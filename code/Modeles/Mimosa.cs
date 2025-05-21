public class Mimosa : PlanteNonComestible
{
    // Constructeur de Mimosa
    public Mimosa()
        : base("Mimosa", 4, 12, TypeTerrain.Sableux) // Nom, durée de pousse, durée de vie maximale, affinité terrain
    {
    }

    // Retourne l'icône spécifique au mimosa
    public override string ObtenirIcone()
    {
        // Le mimosa a une icône spécifique une fois mature
        if (EstMature())
        {
            return "🌺";
        }
        return "🌱"; // Icône semis/croissance
    }
}
