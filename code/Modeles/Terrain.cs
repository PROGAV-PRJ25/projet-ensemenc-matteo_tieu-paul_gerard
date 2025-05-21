
// Classe de base abstraite pour les terrains du jardin
public abstract class Terrain
{
    // Nombre maximum de plantes par terrain
    private const int CAPACITE_MAX_PLANTES = 9;

    // Propriétés du terrain
    public Plante[] Plantes { get; private set; } 
    public bool[] CasesMauvaiseHerbe { get; private set; } 
    public Infrastructure InfrastructureInstallee { get; set; } 
    public TypeTerrain Type { get; protected set; } // Type de terrain

    public Terrain()
    {
        Plantes = new Plante[CAPACITE_MAX_PLANTES]; 
        CasesMauvaiseHerbe = new bool[CAPACITE_MAX_PLANTES]; // Initialise le tableau de mauvaise herbe
        InfrastructureInstallee = null; 
    }

    public bool AjouterPlante(Plante plante, int indexCase)
    {
        // Vérifie si l'index est valide et la case est libre
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] == null && !CasesMauvaiseHerbe[indexCase])
        {
            Plantes[indexCase] = plante;
            return true; // La plante a été ajoutée avec succès
        }
        return false; 
    }

    public Plante RetirerPlante(int indexCase)
    {
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] != null)
        {
            Plante plante = Plantes[indexCase];
            Plantes[indexCase] = null; // Supprime la plante de la case
            return plante; // Retourne la plante retirée
        }
        return null; // Aucune plante à retirer ou index invalide
    }

    public bool EstCaseVide(int indexCase)
    {
        return indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] == null;
    }

    public bool AjouterMauvaiseHerbe(int indexCase)
    {
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] == null && !CasesMauvaiseHerbe[indexCase])
        {
            CasesMauvaiseHerbe[indexCase] = true;
            return true;
        }
        return false;
    }

    // Méthode pour désherber une case
    public bool DesherberCase(int indexCase)
    {
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && CasesMauvaiseHerbe[indexCase])
        {
            CasesMauvaiseHerbe[indexCase] = false;
            return true;
        }
        return false;
    }
}
