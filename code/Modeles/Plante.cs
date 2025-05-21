
public abstract class Plante
{
    private static int _compteurId = 0; // Compteur pour générer des IDs uniques

    // Propriétés de la plante
    public int Id { get; private set; } // Identifiant unique de la plante
    public string Nom { get; protected set; } // Nom de la plante
    public int Sante { get; set; } // Santé de la plante en pourcentage (0-100)
    public int PointsCroissance { get; set; } // Points de croissance accumulés
    public int DureePousseBase { get; protected set; } // Durée de pousse de base en tours
    public int ToursRestantsAvantMaturite { get; set; } // Nombre de tours restants avant que la plante atteigne sa maturité
    public TypeTerrain TypeTerrainAffinite { get; protected set; } // Type de terrain préféré par la plante
    public bool EstMalade { get; set; } // Indique si la plante est malade

    // Constructeur de la classe Plante
    public Plante(string nom, int dureePousseBase, TypeTerrain typeTerrainAffinite)
    {
        Id = ++_compteurId; // Incrémente le compteur et assigne l'ID
        Nom = nom;
        Sante = 100; // Santé initiale de 100%
        PointsCroissance = 0;
        DureePousseBase = dureePousseBase;
        ToursRestantsAvantMaturite = dureePousseBase;
        TypeTerrainAffinite = typeTerrainAffinite;
        EstMalade = false;
    }

    // Méthode abstraite pour obtenir l'icône de la plante (sera implémentée par les classes dérivées)
    public abstract string ObtenirIcone();

    // Méthode pour mettre à jour la santé de la plante
    public void MettreAJourSante(int modification)
    {
        Sante = Math.Clamp(Sante + modification, 0, 100); // S'assure que la santé reste entre 0 et 100
    }

    // Méthode pour faire pousser la plante
    public void Pousser(int points)
    {
        PointsCroissance += points;
        // Met à jour les tours restants avant maturité en fonction des points de croissance
        ToursRestantsAvantMaturite = Math.Max(0, DureePousseBase - (PointsCroissance / 10)); // Exemple de calcul, peut être ajusté
    }

    // Méthode pour vérifier si la plante est mature (sera surchargée pour les plantes comestibles)
    public virtual bool EstMature()
    {
        return ToursRestantsAvantMaturite == 0;
    }

    // Méthode pour réinitialiser l'ID des plantes
    public static void ReinitialiserCompteurId()
    {
        _compteurId = 0;
    }
}
