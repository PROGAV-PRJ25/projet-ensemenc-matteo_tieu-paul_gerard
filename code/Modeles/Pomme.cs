public class Pomme : PlanteComestible
{
    // Constructeur de Pomme
    public Pomme()
        : base("Pomme", 8, 20, TypeTerrain.Terre) // Nom, durée de pousse, durée de vie après maturation, affinité terrain
    {
    }

    // Retourne l'icône spécifique à la pomme mature
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🍏"; // Icône pomme mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
