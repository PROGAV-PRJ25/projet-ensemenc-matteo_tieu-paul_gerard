

public class Fraise : PlanteComestible
{
    // Constructeur de Fraise
    public Fraise()
        : base("Fraise", 3, 8, TypeTerrain.Terre) // Nom, durée de pousse, durée de vie après maturation, affinité terrain
    {
    }

    // Retourne l'icône spécifique à la fraise mature
    public override string ObtenirIcone()
    {
        if (EstMature())
        {
            return "🍓"; // Icône fraise mature
        }
        return "🌱"; // Icône semis/croissance
    }
}
