
public class FleurOrnementale : PlanteNonComestible
{
    // Constructeur de FleurOrnementale
    public FleurOrnementale()
        : base("Fleur Ornementale", 3, 5, TypeTerrain.Sableux) // Nom, durée de pousse, durée de vie maximale, affinité terrain
    {
    }

    // Retourne l'icône spécifique à la fleur ornementale
    public override string ObtenirIcone()
    {
        // Les fleurs ornementales ont une icône spécifique une fois matures
        if (EstMature())
        {
            return "🌸";
        }
        return "🌱"; // Icône semis/croissance
    }
}
