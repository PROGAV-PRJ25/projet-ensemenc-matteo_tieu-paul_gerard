public abstract class Terrain
{
    // Nombre maximum de plantes par terrain
    private static readonly int CAPACITE_MAX_PLANTES = 9;

    // Tableau des plantes du terrain
    public Plante[] Plantes { get; private set; }

    // Tableau des cases mises en place
    public bool[] CasesMauvaiseHerbe { get; private set; }

    // Protection mise en place
    public Infrastructure InfrastructureInstallee { get; set; }

    // Type de terrain
    public TypeTerrain Type { get; protected set; }

    // Constructeur de terrain
    public Terrain()
    {
        Plantes = new Plante[CAPACITE_MAX_PLANTES];
        CasesMauvaiseHerbe = new bool[CAPACITE_MAX_PLANTES];
        InfrastructureInstallee = null;
    }

    // Ajoute une plante
    public bool AjouterPlante(Plante plante, int indexCase)
    {
        // Vérifie si l'index est valide et la case est libre
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] == null && !CasesMauvaiseHerbe[indexCase])
        {
            Plantes[indexCase] = plante;
            return true;
        }
        return false;
    }

    // Supprime la plante de la case
    public Plante RetirerPlante(int indexCase)
    {
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] != null)
        {
            Plante plante = Plantes[indexCase];
            Plantes[indexCase] = null;
            return plante;
        }
        return null; // Aucune plante à retirer ou index invalide
    }

    // Vérifie si la case est vide
    public bool EstCaseVide(int indexCase)
    {
        return indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] == null;
    }

    // Ajoute une mauvaise herbe sur une case donnée
    public bool AjouterMauvaiseHerbe(int indexCase)
    {
        if (indexCase >= 0 && indexCase < CAPACITE_MAX_PLANTES && Plantes[indexCase] == null && !CasesMauvaiseHerbe[indexCase])
        {
            CasesMauvaiseHerbe[indexCase] = true;
            return true;
        }
        return false;
    }

    // Enlève la mauvaise herbe d'une case donnée
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
