public class Nuisible
{
    // Nom du nuisible
    public string Nom { get; private set; }

    // Constructeur de Nuisible
    public Nuisible(string nom)
    {
        Nom = nom;
    }

    // Renvoie l'icône du nuisible
    public string ObtenirIcone()
    {
        return "🐛";
    }
}