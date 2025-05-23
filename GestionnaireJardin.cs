public class GestionnaireJardin
{
    private Terrain[] _terrains; // Les terrains du jardin
    private Random _random = new Random(); // Générateur de nombres aléatoires
    public GestionnaireJardin(Terrain[] terrains)
    {
        _terrains = terrains;
    }

    // Ajoute une plante dans une case donnée
    public bool Planter(TypePlante typePlante, int indexTerrain, int indexCase)
    {
        // Vérifie la validité des indices
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length || indexCase < 0 || indexCase >= _terrains[indexTerrain].Plantes.Length)
        {
            return false;
        }

        Terrain terrainCible = _terrains[indexTerrain];

        // Vérifie si la case est vide et sans mauvaise herbe
        if (!terrainCible.EstCaseVide(indexCase) || terrainCible.CasesMauvaiseHerbe[indexCase])
        {
            return false;
        }

        Plante nouvellePlante;
        // Crée une nouvelle instance de plante en fonction du type choisi
        switch (typePlante)
        {
            case TypePlante.Carotte:
                nouvellePlante = new Carotte();
                break;
            case TypePlante.Fraise:
                nouvellePlante = new Fraise();
                break;
            case TypePlante.Pomme:
                nouvellePlante = new Pomme();
                break;
            case TypePlante.PommeDeTerre:
                nouvellePlante = new PommeDeTerre();
                break;
            case TypePlante.Tomate:
                nouvellePlante = new Tomate();
                break;
            case TypePlante.FleurOrnementale:
                nouvellePlante = new FleurOrnementale();
                break;
            case TypePlante.Lavande:
                nouvellePlante = new Lavande();
                break;
            case TypePlante.Mimosa:
                nouvellePlante = new Mimosa();
                break;
            case TypePlante.Rose:
                nouvellePlante = new Rose();
                break;
            case TypePlante.Tournesol:
                nouvellePlante = new Tournesol();
                break;
            default:
                return false;
        }

        return terrainCible.AjouterPlante(nouvellePlante, indexCase);
    }

    // Arrose une case donnée
    public bool Arroser(int indexTerrain, int indexCase)
    {
        // Vérifie la validité des indices
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length || indexCase < 0 || indexCase >= _terrains[indexTerrain].Plantes.Length)
        {
            return false;
        }

        Plante planteCible = _terrains[indexTerrain].Plantes[indexCase];

        // Vérifie si une plante est présente sur la case
        if (planteCible != null)
        {
            planteCible.MettreAJourSante(20); // Augmente la santé de la plante de 20%
            return true;
        }
        return false; 
    }

    // Retire la mauvaise herbe d'une case donnée
    public bool Desherber(int indexTerrain, int indexCase)
    {
        // Vérifie la validité des indices
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length || indexCase < 0 || indexCase >= _terrains[indexTerrain].Plantes.Length)
        {
            return false;
        }

        Terrain terrainCible = _terrains[indexTerrain];

        return terrainCible.DesherberCase(indexCase);
    }

    // Récolte une plante
    public Plante Recolter(int indexTerrain, int indexCase)
    {
        // Vérifie la validité des indices
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length || indexCase < 0 || indexCase >= _terrains[indexTerrain].Plantes.Length)
        {
            return null;
        }

        Terrain terrainCible = _terrains[indexTerrain];
        Plante planteCible = terrainCible.Plantes[indexCase];

        // Vérifie si une plante est présente et si elle est comestible et mature
        if (planteCible is PlanteComestible planteComestible && planteComestible.EstMature())
        {
            return terrainCible.RetirerPlante(indexCase); // Retire et retourne la plante récoltée
        }
        return null; 
    }

    // Traite la maladie de la plante
    public bool TraiterMaladie(int indexTerrain, int indexCase)
    {
        // Vérifie la validité des indices
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length || indexCase < 0 || indexCase >= _terrains[indexTerrain].Plantes.Length)
        {
            return false;
        }

        Plante planteCible = _terrains[indexTerrain].Plantes[indexCase];

        // Vérifie si une plante est présente et qu'elle est malade
        if (planteCible != null && planteCible.EstMalade)
        {
            planteCible.EstMalade = false; // Guérit la plante
            planteCible.MettreAJourSante(15); // Donne un petit boost de santé après traitement
            return true;
        }
        return false; // Aucune plante malade à traiter ou pas de plante
    }

    // Mettre des infrastructure dans le jardin
    public bool InstallerInfrastructure(TypeEvenement typeInfrastructure, int indexTerrain)
    {
        // Vérifie la validité de l'index du terrain
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length)
        {
            return false;
        }

        Terrain terrainCible = _terrains[indexTerrain];

        // Vérifie si une infrastructure est déjà installée sur ce terrain
        if (terrainCible.InfrastructureInstallee != null && terrainCible.InfrastructureInstallee.EstActive())
        {
            return false; 
        }

        terrainCible.InfrastructureInstallee = new Infrastructure(typeInfrastructure);
        return true;
    }

    // Applique un fertilisant à une plante
    public bool UtiliserFertilisant(int indexTerrain, int indexCase)
    {
        if (indexTerrain < 0 || indexTerrain >= _terrains.Length || indexCase < 0 || indexCase >= _terrains[indexTerrain].Plantes.Length)
        {
            return false;
        }

        Plante planteCible = _terrains[indexTerrain].Plantes[indexCase];

        if (planteCible != null)
        {
            // Un fertilisant accélère la croissance (ici, nous l'implémentons en réduisant directement les tours restants)
            planteCible.ToursRestantsAvantMaturite = Math.Max(0, planteCible.ToursRestantsAvantMaturite - 3); // Par exemple, réduit de 3 tours
            return true;
        }
        return false;
    }
}
