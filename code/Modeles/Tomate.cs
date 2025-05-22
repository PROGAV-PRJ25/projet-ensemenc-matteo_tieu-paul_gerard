public class Tomate : PlanteComestible
{
    // Constructeur de Tomate
    public Tomate() : base("Tomate", 5, 10, TypeTerrain.Terre) { }
    // Nom, durée de pousse, durée de vie après maturation, affinité terrain

    // Retourne l'icône représentant la tomate mature
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🍅"; // Icône tomate mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
