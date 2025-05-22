public class Carotte : PlanteComestible
{
    // Constructeur de Carotte
    public Carotte() : base("Carotte", 4, 6, TypeTerrain.Sableux) { }
    // Nom, durée de pousse, durée de vie après maturation, affinité terrain

    // Retourne l'icône représentant la carotte
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🥕"; // Icône carotte mature
        }
        return "🌱"; // Icône semis/croissance
    }
}