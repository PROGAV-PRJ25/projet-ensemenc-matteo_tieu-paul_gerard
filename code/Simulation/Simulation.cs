// Classe principale du simulateur qui gère le déroulement du jeu
public class Simulation
{
    private static readonly int NOMBRETERRAINS = 3;

    private static readonly int ACTIONSPARTOUR = 5;

    // Tableau des terrains du jardin
    private Terrain[] terrains;

    // Service de gestion des actions du joueur sur le jardin
    private GestionnaireJardin gestionnaireJardin;

    // Service d'affichage en console
    private AffichageConsole affichageConsole;

    private Random random;

    // Numéro du tour actuel
    private int numeroTour;

    private TypeSaison saisonActuelle;

    private TypeClimat climatActuel;

    private TypeEvenement evenementEnCours;

    // Nombre de tours restants avant la fin d'un événement
    private int toursRestantsEvenement;

    // Nombre de tours où le joueur est épuisé et ne peux plus jouer
    private int toursEpuisementJoueur;

    private int scoreJoueur;

    private int plantesRecolteesTotal;

    private int plantesMortesTotal;

    // Titre honorifique actuel du joueur (bonus)
    private TitreJoueur titreActuel;

    // Constructeur de Simulation
    public Simulation()
    {
        terrains = new Terrain[NOMBRETERRAINS]
        {
                new TerrainDeTerre(),
                new TerrainSableux(),
                new TerrainArgileux()
        };
        gestionnaireJardin = new GestionnaireJardin(terrains);
        affichageConsole = new AffichageConsole();
        random = new Random();
        numeroTour = 1;
        saisonActuelle = TypeSaison.Printemps; // Le jeu commence au printemps (arbitraire)
        climatActuel = TypeClimat.Neutre; // Le climat est neutre au débart
        evenementEnCours = TypeEvenement.Aucun;
        toursRestantsEvenement = 0;
        toursEpuisementJoueur = 0;
        scoreJoueur = 0;
        plantesRecolteesTotal = 0;
        plantesMortesTotal = 0;
        titreActuel = new TitreJoueur();
    }

    // Démarre le jeu
    public void DemarrerJeu()
    {
        affichageConsole.AfficherMessageBienvenue();

        // Boucle principale du jeu
        while (true)
        {
            // Vérifie si le joueur est épuisé
            if (toursEpuisementJoueur > 0)
            {
                affichageConsole.AfficherMessage("Vous êtes épuisé et ne pouvez pas agir. Tours restants : " + toursEpuisementJoueur);
                AvancerTour();
                continue; // Passe au tour suivant directement
            }

            // Gère les événements en cours
            if (evenementEnCours != TypeEvenement.Aucun)
            {
                GererEvenementEnCours();
                if (evenementEnCours != TypeEvenement.Aucun) // Si l'événement est toujours actif, le joueur ne peut pas effectuer les actions comme d'habitude
                {
                    AvancerTour();
                    continue;
                }
            }

            affichageConsole.AfficherEtatJardin(numeroTour, saisonActuelle, climatActuel, titreActuel.NomTitre, terrains);
            affichageConsole.AfficherMenuPrincipal(ACTIONSPARTOUR);
            int actionsRestantes = ACTIONSPARTOUR;
            // Boucle pour les actions du joueur pendant le tour
            while (actionsRestantes > 0)
            {
                Console.WriteLine($"🎮 Actions restantes ce tour : {actionsRestantes}");
                string choixAction = Console.ReadLine();

                if (choixAction == "7") // "Quitter"
                {
                    affichageConsole.AfficherMessage("Merci d'avoir joué !");
                    return;
                }

                if (int.TryParse(choixAction, out int action) && action >= 1 && action <= 6)
                {
                    if (action != 6) // Action différente de "Voir l'état de mes plantes", qui ne nécessite pas de sélectionner un terrain
                    {
                        affichageConsole.AfficherSelectionTerrain();
                        if (!int.TryParse(Console.ReadLine(), out int indexTerrain) || indexTerrain < 1 || indexTerrain > NOMBRETERRAINS)
                        {
                            affichageConsole.AfficherMessage("Sélection de terrain invalide.");
                            continue;
                        }
                        indexTerrain--;

                        bool actionReussie = ExecuterActionJoueur(action, indexTerrain);
                        if (actionReussie)
                        {
                            actionsRestantes--;
                        }
                        else
                        {
                            affichageConsole.AfficherMessage("Action impossible ou invalide.");
                        }
                    }
                    else // "Voir l'état de mes plantes"
                    {
                        affichageConsole.AfficherEtatDetaillePlantes(terrains);
                    }
                }
                else
                {
                    affichageConsole.AfficherMessage("Choix invalide. Veuillez réessayer.");
                }
                affichageConsole.DemanderEntreeUtilisateur("Appuyez sur Entrée pour continuer...");
                affichageConsole.NettoyerConsole();
                affichageConsole.AfficherEtatJardin(numeroTour, saisonActuelle, climatActuel, titreActuel.NomTitre, terrains);
                affichageConsole.AfficherMenuPrincipal(actionsRestantes);
            }

            AvancerTour();
        }
    }

    // Exécute l'action choisie par le joueur
    private bool ExecuterActionJoueur(int action, int indexTerrain)
    {
        switch (action)
        {
            case 1: // "Planter une plante"
                affichageConsole.AfficherMenuPlanter();
                if (int.TryParse(Console.ReadLine(), out int choixPlante))
                {
                    TypePlante? typePlanteChoisie = ObtenirTypePlante(choixPlante);
                    if (typePlanteChoisie.HasValue)
                    {
                        return DemanderEmplacementEtPlanter(typePlanteChoisie.Value, indexTerrain);
                    }
                }
                affichageConsole.AfficherMessage("Choix de plante invalide.");
                return false;

            case 2: // "Arroser mes plantes"
                return DemanderEmplacementEtArroser(indexTerrain);

            case 3: // "Désherber"
                return DemanderEmplacementEtDesherber(indexTerrain);

            case 4: // "Récolter"
                return DemanderEmplacementEtRecolter(indexTerrain);

            case 5: // "Traiter contre les maladies"
                return DemanderEmplacementEtTraiterMaladie(indexTerrain);

            case 6: // "Installer une infrastructure"
                affichageConsole.AfficherMenuInstallerInfrastructure();
                if (int.TryParse(Console.ReadLine(), out int choixInfrastructure))
                {
                    TypeEvenement? typeInfraChoisie = ObtenirTypeInfrastructure(choixInfrastructure);
                    if (typeInfraChoisie.HasValue)
                    {
                        return gestionnaireJardin.InstallerInfrastructure(typeInfraChoisie.Value, indexTerrain);
                    }
                }
                affichageConsole.AfficherMessage("Choix d'infrastructure invalide.");
                return false;
            default:
                return false;
        }
    }

    // Obtient le type de plante à partir du choix de l'utilisateur
    private TypePlante? ObtenirTypePlante(int choix)
    {
        return choix switch
        {
            1 => TypePlante.Carotte,
            2 => TypePlante.Tomate,
            3 => TypePlante.PommeDeTerre,
            4 => TypePlante.Pomme,
            5 => TypePlante.Fraise,
            6 => TypePlante.FleurOrnementale,
            7 => TypePlante.Lavande,
            8 => TypePlante.Rose,
            9 => TypePlante.Tournesol,
            10 => TypePlante.Mimosa,
             => null,
        };
    }

    // Obtient le type d'infrastructure à partir du choix de l'utilisateur
    private TypeEvenement? ObtenirTypeInfrastructure(int choix)
    {
        return choix switch
        {
            1 => TypeEvenement.Tempete, // Serre 
            2 => TypeEvenement.Nuisibles, // Barrière 
            3 => TypeEvenement.Canicule, // Pare-soleil
             => null,
        };
    }


    // Demande l'emplacement de la plante à planter
    private bool DemanderEmplacementEtPlanter(TypePlante typePlante, int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        affichageConsole.AfficherCasesDisponibles(terrainCible, typePlante, indexTerrain);

        if (int.TryParse(Console.ReadLine(), out int indexCase) && indexCase >= 1 && indexCase <= 9)
        {
            return gestionnaireJardin.Planter(typePlante, indexTerrain, indexCase - 1);
        }
        affichageConsole.AfficherMessage("Emplacement invalide.");
        return false;
    }

    // Demande l'emplacement de la plante à arroser
    private bool DemanderEmplacementEtArroser(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        affichageConsole.AfficherPlantesPourAction("arroser", terrainCible, indexTerrain);

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            return gestionnaireJardin.Arroser(indexTerrain, choix - 1); // Ajuste l'index
        }
        affichageConsole.AfficherMessage("Emplacement invalide.");
        return false;
    }

    // Demande l'emplacement de la mauvaise herbe à désherber
    private bool DemanderEmplacementEtDesherber(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        affichageConsole.AfficherCasesMauvaiseHerbe(terrainCible, indexTerrain);

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            return gestionnaireJardin.Desherber(indexTerrain, choix - 1);
        }
        affichageConsole.AfficherMessage("Emplacement invalide.");
        return false;
    }

    // Demande l'emplacement de la plante à récolter
    private bool DemanderEmplacementEtRecolter(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        affichageConsole.AfficherPlantesPourAction("récolter", terrainCible, indexTerrain, "recolte");

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            Plante planteRecoltee = gestionnaireJardin.Recolter(indexTerrain, choix - 1);
            if (planteRecoltee != null)
            {
                plantesRecolteesTotal++;
                scoreJoueur += 10;
                titreActuel.MettreAJourTitre(scoreJoueur);
                affichageConsole.AfficherMessage($"Vous avez récolté {planteRecoltee.Nom} ! (+10 points)");
                return true;
            }
        }
        affichageConsole.AfficherMessage("Impossible de récolter à cet emplacement ou pas de plante mature.");
        return false;
    }

    // Demande l'emplacement de la plante à traiter contre la maladie
    private bool DemanderEmplacementEtTraiterMaladie(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        affichageConsole.AfficherPlantesPourAction("traiter", terrainCible, indexTerrain, "maladie");

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            return gestionnaireJardin.TraiterMaladie(indexTerrain, choix - 1); // Ajuste l'index
        }
        affichageConsole.AfficherMessage("Emplacement invalide ou plante non malade.");
        return false;
    }
}