public abstract class Plante
{
    // Compteur pour générer des IDs uniques
    private static int _compteurId = 0;

    // Identifiant unique de la plante
    public int Id { get; private set; }

    // Nom de la plante
    public string Nom { get; protected set; }

    // Santé de la plante (en %)
    public int Sante { get; set; }

    // Points de croissance accumulés
    public int PointsCroissance { get; set; }

    // Durée de pousse de base en tours
    public int DureePousseBase { get; protected set; }

    // Nombre de tours restants avant que la plante atteigne sa maturité
    public int ToursRestantsAvantMaturite { get; set; }

    // Type de terrain préféré par la plante
    public TypeTerrain TypeTerrainAffinite { get; protected set; }

    // Indique si la plante est malade
    public bool EstMalade { get; set; }

    // Constructeur de la classe Plante
    public Plante(string nom, int dureePousseBase, TypeTerrain typeTerrainAffinite)
    {
        Id = ++_compteurId;
        Nom = nom;
        Sante = 100;
        PointsCroissance = 0;
        DureePousseBase = dureePousseBase;
        ToursRestantsAvantMaturite = dureePousseBase;
        TypeTerrainAffinite = typeTerrainAffinite;
        EstMalade = false;
    }

    // Méthode abstraite pour obtenir l'icône de la plante (sera implémentée par les classes dérivées)
    public abstract string ObtenirIcone();

    // Met à jour la santé de la plante
    public void MettreAJourSante(int modification)
    {
        Sante = Math.Clamp(Sante + modification, 0, 100); // S'assure que la santé reste entre 0 et 100
    }

    // Fait pousser la plante
    public void Pousser(int points)
    {
        PointsCroissance += points;
        ToursRestantsAvantMaturite = Math.Max(0, DureePousseBase - (PointsCroissance / 10)); // Pas sûr du calcul, ça peut être changé
    }

    // Vérifie si la plante est mature
    public bool EstMature()
    {
        return ToursRestantsAvantMaturite == 0;
    }

    // Réinitialise l'ID des plantes
    public static void ReinitialiserCompteurId()
    {
        _compteurId = 0;
    }
}
