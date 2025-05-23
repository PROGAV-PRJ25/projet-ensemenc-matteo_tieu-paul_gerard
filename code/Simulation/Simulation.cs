// Classe principale du simulateur qui gère le déroulement du jeu
public class Simulation
{
    private const int NOMBRETERRAINS = 3;

    private static readonly int ACTIONSPARTOUR = 5;

    // Tableau des terrains du jardin
    private Terrain[] terrains;

    // Service de gestion des actions du joueur sur le jardin
    private GestionnaireJardin gestionnaireJardin;

    // Service d'affichage en console
    private Affichage Affichage;

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
        Affichage = new Affichage();
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
        Affichage.AfficherMessageBienvenue();

        // Boucle principale du jeu
        while (true)
        {
            // Vérifie si le joueur est épuisé
            if (toursEpuisementJoueur > 0)
            {
                Affichage.AfficherMessage("Vous êtes épuisé et ne pouvez pas agir. Tours restants : " + toursEpuisementJoueur);
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

            Affichage.AfficherEtatJardin(numeroTour, saisonActuelle, climatActuel, titreActuel.NomTitre, terrains);
            Affichage.AfficherMenuPrincipal(ACTIONSPARTOUR);
            int actionsRestantes = ACTIONSPARTOUR;
            // Boucle pour les actions du joueur pendant le tour
            while (actionsRestantes > 0)
            {
                Console.WriteLine($"🎮 Actions restantes ce tour : {actionsRestantes}");
                string choixAction = Console.ReadLine();

                if (choixAction == "7") // "Quitter"
                {
                    Affichage.AfficherMessage("Merci d'avoir joué !");
                    return;
                }

                if (int.TryParse(choixAction, out int action) && action >= 1 && action <= 6)
                {
                    if (action != 6) // Action différente de "Voir l'état de mes plantes", qui ne nécessite pas de sélectionner un terrain
                    {
                        Affichage.AfficherSelectionTerrain();
                        if (!int.TryParse(Console.ReadLine(), out int indexTerrain) || indexTerrain < 1 || indexTerrain > NOMBRETERRAINS)
                        {
                            Affichage.AfficherMessage("Sélection de terrain invalide.");
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
                            Affichage.AfficherMessage("Action impossible ou invalide.");
                        }
                    }
                    else // "Voir l'état de mes plantes"
                    {
                        Affichage.AfficherEtatDetaillePlantes(terrains);
                    }
                }
                else
                {
                    Affichage.AfficherMessage("Choix invalide. Veuillez réessayer.");
                }
                Affichage.DemanderEntreeUtilisateur("Appuyez sur Entrée pour continuer...");
                Affichage.NettoyerConsole();
                Affichage.AfficherEtatJardin(numeroTour, saisonActuelle, climatActuel, titreActuel.NomTitre, terrains);
                Affichage.AfficherMenuPrincipal(actionsRestantes);
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
                Affichage.AfficherMenuPlanter();
                if (int.TryParse(Console.ReadLine(), out int choixPlante))
                {
                    TypePlante? typePlanteChoisie = ObtenirTypePlante(choixPlante);
                    if (typePlanteChoisie.HasValue)
                    {
                        return DemanderEmplacementEtPlanter(typePlanteChoisie.Value, indexTerrain);
                    }
                }
                Affichage.AfficherMessage("Choix de plante invalide.");
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
                Affichage.AfficherMenuInstallerInfrastructure();
                if (int.TryParse(Console.ReadLine(), out int choixInfrastructure))
                {
                    TypeEvenement? typeInfraChoisie = ObtenirTypeInfrastructure(choixInfrastructure);
                    if (typeInfraChoisie.HasValue)
                    {
                        return gestionnaireJardin.InstallerInfrastructure(typeInfraChoisie.Value, indexTerrain);
                    }
                }
                Affichage.AfficherMessage("Choix d'infrastructure invalide.");
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
            _ => null,
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
            _ => null,
        };
    }


    // Demande l'emplacement de la plante à planter
    private bool DemanderEmplacementEtPlanter(TypePlante typePlante, int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        Affichage.AfficherCasesDisponibles(terrainCible, typePlante, indexTerrain);

        if (int.TryParse(Console.ReadLine(), out int indexCase) && indexCase >= 1 && indexCase <= 9)
        {
            return gestionnaireJardin.Planter(typePlante, indexTerrain, indexCase - 1);
        }
        Affichage.AfficherMessage("Emplacement invalide.");
        return false;
    }

    // Demande l'emplacement de la plante à arroser
    private bool DemanderEmplacementEtArroser(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        Affichage.AfficherPlantesPourAction("arroser", terrainCible, indexTerrain);

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            return gestionnaireJardin.Arroser(indexTerrain, choix - 1); // Ajuste l'index
        }
        Affichage.AfficherMessage("Emplacement invalide.");
        return false;
    }

    // Demande l'emplacement de la mauvaise herbe à désherber
    private bool DemanderEmplacementEtDesherber(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        Affichage.AfficherCasesMauvaiseHerbe(terrainCible, indexTerrain);

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            return gestionnaireJardin.Desherber(indexTerrain, choix - 1);
        }
        Affichage.AfficherMessage("Emplacement invalide.");
        return false;
    }

    // Demande l'emplacement de la plante à récolter
    private bool DemanderEmplacementEtRecolter(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        Affichage.AfficherPlantesPourAction("récolter", terrainCible, indexTerrain, "recolte");

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            Plante planteRecoltee = gestionnaireJardin.Recolter(indexTerrain, choix - 1);
            if (planteRecoltee != null)
            {
                plantesRecolteesTotal++;
                scoreJoueur += 10;
                titreActuel.MettreAJourTitre(scoreJoueur);
                Affichage.AfficherMessage($"Vous avez récolté {planteRecoltee.Nom} ! (+10 points)");
                return true;
            }
        }
        Affichage.AfficherMessage("Impossible de récolter à cet emplacement ou pas de plante mature.");
        return false;
    }

    // Demande l'emplacement de la plante à traiter contre la maladie
    private bool DemanderEmplacementEtTraiterMaladie(int indexTerrain)
    {
        Terrain terrainCible = terrains[indexTerrain];
        Affichage.AfficherPlantesPourAction("traiter", terrainCible, indexTerrain, "maladie");

        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 9)
        {
            return gestionnaireJardin.TraiterMaladie(indexTerrain, choix - 1); // Ajuste l'index
        }
        Affichage.AfficherMessage("Emplacement invalide ou plante non malade.");
        return false;
    }

    // Fait avancer le jeu d'un tour
    private void AvancerTour()
    {
        numeroTour++;
        Affichage.AfficherMessage("--- Fin du tour ! ---");
        MettreAJourSaison();
        GenererEvenementAleatoire();
        MettreAJourEtatsPlantes();
        VerifierMortPlantes();
        GererMauvaiseHerbeAleatoire();
        AfficherRecapitulatifTour();
        Affichage.DemanderEntreeUtilisateur("Appuyez sur Entrée pour passer au tour suivant...");
        Affichage.NettoyerConsole();
    }

    // Génère un type de climat aléatoire (Avantageux, Neutre, Désavantageux, ou Gel si Hiver)
    private TypeClimat GenererClimat(Random random, TypeSaison saison = TypeSaison.Printemps)
    {
        int chance = random.Next(1, 101);

        if (saison == TypeSaison.Hiver && chance <= 20)
        {
            return TypeClimat.Gel;
        }
        else if (chance <= 30)
        {
            return TypeClimat.Avantageux;
        }
        else if (chance <= 60)
        {
            return TypeClimat.Desavantageux;
        }
        else
        {
            return TypeClimat.Neutre;
        }
    }

    // Met à jour la saison en fonction du numéro de tour
    private void MettreAJourSaison()
    {
        // Chaque saison dure 10 tours
        int toursParSaison = 10;
        switch ((numeroTour - 1) / toursParSaison % 4)
        {
            case 0: saisonActuelle = TypeSaison.Printemps; break;
            case 1: saisonActuelle = TypeSaison.Ete; break;
            case 2: saisonActuelle = TypeSaison.Automne; break;
            case 3: saisonActuelle = TypeSaison.Hiver; break;
        }
        climatActuel = GenererClimat(random, saisonActuelle);
    }

    private void GenererEvenementAleatoire()
    {
        // Seulement si aucun événement n'est en cours et si le joueur n'est pas épuisé
        if (evenementEnCours == TypeEvenement.Aucun && toursEpuisementJoueur == 0)
        {
            int chanceEvenement = random.Next(1, 101);
            if (chanceEvenement <= 10)
            {
                int type = random.Next(1, 5); // 1: Tempête, 2: Canicule, 3: Nuisibles, 4: Maladie
                switch (type)
                {
                    case 1: evenementEnCours = TypeEvenement.Tempete; toursRestantsEvenement = 3; break;
                    case 2: evenementEnCours = TypeEvenement.Canicule; toursRestantsEvenement = 3; break;
                    case 3: evenementEnCours = TypeEvenement.Nuisibles; toursRestantsEvenement = 1; break;
                    case 4: evenementEnCours = TypeEvenement.Maladie; toursRestantsEvenement = 3; break;
                }
                Affichage.AfficherMessage($"🚨 ALERTE ! 🚨 {evenementEnCours.ToString().ToUpper()} FAIT RAGE SUR VOTRE JARDIN !");

                if (evenementEnCours == TypeEvenement.Tempete || evenementEnCours == TypeEvenement.Canicule ||
                    evenementEnCours == TypeEvenement.Nuisibles || evenementEnCours == TypeEvenement.Maladie)
                {
                    GererModeUrgence(evenementEnCours);
                }
            }
        }
    }

    // Gère les événements actifs
    private void GererEvenementEnCours()
    {
        if (evenementEnCours != TypeEvenement.Aucun)
        {
            // Vérifie si une infrastructure protège de cet événement
            bool estProtege = false;
            foreach (var terrain in terrains)
            {
                if (terrain.InfrastructureInstallee != null &&
                    terrain.InfrastructureInstallee.EstActive() &&
                    terrain.InfrastructureInstallee.TypeProtection == evenementEnCours)
                {
                    Affichage.AfficherMessage($"Le terrain {Array.IndexOf(terrains, terrain) + 1} est protégé par son infrastructure !");
                    terrain.InfrastructureInstallee.DiminuerProtection();
                    estProtege = true;
                    break; // Une seule protection suffit pour l'événement global
                }
            }

            if (!estProtege)
            {
                // Si l'événement est Nuisibles ou Maladie, et non protégé
                if (evenementEnCours == TypeEvenement.Nuisibles || evenementEnCours == TypeEvenement.Maladie)
                {
                    // Pas encore implémenté
                    // Les dégâts sont gérés au moment du déclenchement initial de l'événement en mode urgence
                    // Ici, on diminue juste la durée restante.
                }
            }

            toursRestantsEvenement--;

            if (toursRestantsEvenement <= 0)
            {
                Affichage.AfficherMessage($"La {evenementEnCours} s'est calmée. Retour au Mode Normal.");
                evenementEnCours = TypeEvenement.Aucun;
            }
        }

        if (toursEpuisementJoueur > 0)
        {
            toursEpuisementJoueur--;
            if (toursEpuisementJoueur == 0)
            {
                Affichage.AfficherMessage("Vous n'êtes plus épuisé et pouvez de nouveau agir.");
            }
        }
    }


    // Gère le mode urgence
    private void GererModeUrgence(TypeEvenement evenement)
    {
        Affichage.AfficherMessage($"🚨 MODE URGENCE : {evenement.ToString().ToUpper()} 🚨");
        if (evenement == TypeEvenement.Nuisibles)
        {
            Affichage.AfficherMenuUrgenceNuisibles();
            string choix = Console.ReadLine();
            if (choix == "1") // Faire du bruit
            {
                Affichage.AfficherMessage("Vous avez passé la nuit à surveiller... Vous êtes épuisé et ne pourrez pas agir pendant 5 tours.");
                toursEpuisementJoueur = 5;
            }
            else // Ne rien faire
            {
                if (random.Next(2) == 0)
                {
                    int plantesPerdues = random.Next(1, 6);
                    Affichage.AfficherMessage($"Vous n'avez rien fait. {plantesPerdues} plantes ont été perdues à cause des nuisibles !");
                    PerdrePlantesAleatoirement(plantesPerdues);
                }
                else
                {
                    Affichage.AfficherMessage("Vous avez eu de la chance, les nuisibles n'ont pas fait de dégâts cette fois-ci.");
                }
            }
        }
        else if (evenement == TypeEvenement.Maladie)
        {
            int plantesAffectees = random.Next(1, 4);
            Affichage.AfficherMessage($"{plantesAffectees} plantes sont tombées malades ! Traitez-les rapidement !");
            AffecterPlantesAleatoirement(plantesAffectees, true);
        }
    }

    // Perd des plantes aléatoirement
    private void PerdrePlantesAleatoirement(int nombre)
    {
        List<Plante> toutesLesPlantes = new List<Plante>();
        foreach (var terrain in terrains)
        {
            toutesLesPlantes.AddRange(terrain.Plantes.Where(p => p != null));
        }

        for (int i = 0; i < nombre; i++)
        {
            if (toutesLesPlantes.Count == 0) break;

            int indexPlanteADetruire = random.Next(toutesLesPlantes.Count);
            Plante planteADetruire = toutesLesPlantes[indexPlanteADetruire];

            // Trouver le terrain et l'index de la plante à détruire
            for (int t = 0; t < terrains.Length; t++)
            {
                for (int c = 0; c < terrains[t].Plantes.Length; c++)
                {
                    if (terrains[t].Plantes[c] == planteADetruire)
                    {
                        terrains[t].Plantes[c] = null;
                        plantesMortesTotal++;
                        scoreJoueur -= 5;
                        titreActuel.MettreAJourTitre(scoreJoueur);
                        Affichage.AfficherMessage($"- {terrains[t].Plantes[c]?.Nom ?? "Une plante"} est morte à cause des nuisibles ! (-5 points)");
                        break;
                    }
                }
                if (terrains[t].Plantes.Contains(planteADetruire)) break;
            }
            toutesLesPlantes.RemoveAt(indexPlanteADetruire);
        }
    }

    // Affecte des plantes aléatoirement (maladie)
    private void AffecterPlantesAleatoirement(int nombre, bool estMaladie)
    {
        List<Plante> toutesLesPlantes = new List<Plante>();
        foreach (var terrain in terrains)
        {
            toutesLesPlantes.AddRange(terrain.Plantes.Where(p => p != null && !p.EstMalade));
        }

        for (int i = 0; i < nombre; i++)
        {
            if (toutesLesPlantes.Count == 0) break;

            int indexPlanteAAffecter = random.Next(toutesLesPlantes.Count);
            Plante planteAAffecter = toutesLesPlantes[indexPlanteAAffecter];

            if (estMaladie)
            {
                planteAAffecter.EstMalade = true;
                Affichage.AfficherMessage($"- {planteAAffecter.Nom} n°{planteAAffecter.Id} est maintenant malade !");
            }
            toutesLesPlantes.RemoveAt(indexPlanteAAffecter);
        }
    }

    // Détermine si un type de terrain est désavantageux pour un autre
    private bool EstDesavantageux(TypeTerrain affinitePlante, TypeTerrain terrainActuel)
    {
        if (affinitePlante == TypeTerrain.Sableux && terrainActuel == TypeTerrain.Argileux) return true;
        if (affinitePlante == TypeTerrain.Argileux && terrainActuel == TypeTerrain.Sableux) return true;
        return false;
    }

    // Obtient l'effet de la saison sur une plante spécifique
    private TypeClimat ObtenirEffetSaison(string nomPlante, TypeSaison saisonActuelle)
    {
        switch (nomPlante)
        {
            case "Tomate":
                return (saisonActuelle == TypeSaison.Printemps || saisonActuelle == TypeSaison.Ete) ? TypeClimat.Avantageux : TypeClimat.Neutre;
            case "Carotte":
                return (saisonActuelle == TypeSaison.Printemps || saisonActuelle == TypeSaison.Automne) ? TypeClimat.Avantageux : TypeClimat.Neutre;
            case "Pomme de terre":
                return (saisonActuelle == TypeSaison.Printemps || saisonActuelle == TypeSaison.Ete) ? TypeClimat.Avantageux :
                       (saisonActuelle == TypeSaison.Automne ? TypeClimat.Desavantageux : TypeClimat.Neutre);
            case "Pomme":
                return (saisonActuelle == TypeSaison.Ete || saisonActuelle == TypeSaison.Automne) ? TypeClimat.Avantageux : TypeClimat.Neutre;
            case "Fraise":
                return (saisonActuelle == TypeSaison.Ete || saisonActuelle == TypeSaison.Automne) ? TypeClimat.Avantageux : TypeClimat.Neutre;
            case "Fleur Ornementale":
                return (saisonActuelle == TypeSaison.Automne || saisonActuelle == TypeSaison.Hiver) ? TypeClimat.Avantageux : TypeClimat.Neutre;
            case "Lavande":
                return (saisonActuelle == TypeSaison.Printemps || saisonActuelle == TypeSaison.Ete || saisonActuelle == TypeSaison.Hiver) ? TypeClimat.Avantageux : TypeClimat.Desavantageux;
            case "Rose":
                return (saisonActuelle == TypeSaison.Ete) ? TypeClimat.Avantageux : TypeClimat.Desavantageux;
            case "Tournesol":
                return (saisonActuelle == TypeSaison.Ete) ? TypeClimat.Avantageux :
                       (saisonActuelle == TypeSaison.Hiver ? TypeClimat.Desavantageux : TypeClimat.Neutre);
            case "Mimosa":
                return (saisonActuelle == TypeSaison.Printemps || saisonActuelle == TypeSaison.Ete || saisonActuelle == TypeSaison.Hiver) ? TypeClimat.Avantageux : TypeClimat.Neutre;
            default:
                return TypeClimat.Neutre;
        }
    }

    // Obtient l'effet de la météo sur une plante spécifique (pas implémenté pour l'instant)
    private TypeClimat ObtenirEffetMeteo(string nomPlante, TypeClimat climatActuel)
    {
        return climatActuel;
    }


    // Met à jour les états de toutes les plantes (croissance, santé)
    private void MettreAJourEtatsPlantes()
    {
        foreach (var terrain in terrains)
        {
            // Diminue la durée de protection des infrastructures
            terrain.InfrastructureInstallee?.DiminuerProtection();

            for (int i = 0; i < terrain.Plantes.Length; i++)
            {
                Plante plante = terrain.Plantes[i];
                if (plante == null) continue;

                int modificateurVitesseCroissance = 0;
                int modificateurSante = 0;

                if (plante.TypeTerrainAffinite == terrain.Type)
                {
                    modificateurVitesseCroissance += 5;
                    modificateurSante += 5;
                }
                else if (EstDesavantageux(plante.TypeTerrainAffinite, terrain.Type))
                {
                    modificateurVitesseCroissance -= 5;
                    modificateurSante -= 5;
                }

                TypeClimat effetSaison = ObtenirEffetSaison(plante.Nom, saisonActuelle);
                if (effetSaison == TypeClimat.Avantageux)
                {
                    modificateurVitesseCroissance += 5;
                    modificateurSante += 5;
                }
                else if (effetSaison == TypeClimat.Desavantageux)
                {
                    modificateurVitesseCroissance -= 5;
                    modificateurSante -= 5;
                }

                TypeClimat effetMeteo = ObtenirEffetMeteo(plante.Nom, climatActuel);
                if (effetMeteo == TypeClimat.Avantageux)
                {
                    modificateurSante += 10;
                }
                else if (effetMeteo == TypeClimat.Desavantageux)
                {
                    modificateurSante -= 10;
                }

                if (evenementEnCours == TypeEvenement.Tempete || evenementEnCours == TypeEvenement.Canicule || climatActuel == TypeClimat.Gel)
                {
                    if (terrain.InfrastructureInstallee == null || !terrain.InfrastructureInstallee.EstActive() || terrain.InfrastructureInstallee.TypeProtection != evenementEnCours)
                    {
                        modificateurSante -= 10;
                    }

                    if (climatActuel == TypeClimat.Gel && (terrain.InfrastructureInstallee == null || !terrain.InfrastructureInstallee.EstActive()))
                    {
                        modificateurSante -= 10;
                    }
                }

                if (plante.EstMalade)
                {
                    modificateurSante -= 10;
                }


                plante.MettreAJourSante(modificateurSante);
                plante.Pousser(10 + modificateurVitesseCroissance);

                if (plante is PlanteComestible planteComestible)
                {
                    if (planteComestible.EstMature())
                    {
                        planteComestible.ToursDepuisMaturation++;
                    }
                }
                else if (plante is PlanteNonComestible planteNonComestible)
                {
                    planteNonComestible.ToursEcoules++;
                }
            }
        }
    }

    // Vérifie et gère la mort des plantes
    private void VerifierMortPlantes()
    {
        foreach (var terrain in terrains)
        {
            for (int i = 0; i < terrain.Plantes.Length; i++)
            {
                Plante plante = terrain.Plantes[i];
                if (plante == null) continue;

                bool estMorte = false;
                if (plante.Sante <= 0)
                {
                    estMorte = true;
                    Affichage.AfficherMessage($"- {plante.Nom} n°{plante.Id} est morte de maladie ou négligence !");
                }
                else if (plante is PlanteComestible pc && pc.EstMorteDeVieillesse())
                {
                    estMorte = true;
                    Affichage.AfficherMessage($"- {plante.Nom} n°{plante.Id} est morte de vieillesse !");
                }
                else if (plante is PlanteNonComestible pnc && pnc.EstMorteDeVieillesse())
                {
                    estMorte = true;
                    Affichage.AfficherMessage($"- {plante.Nom} n°{plante.Id} a terminé son cycle de vie.");
                }

                if (estMorte)
                {
                    terrain.Plantes[i] = null;
                    plantesMortesTotal++;
                    scoreJoueur -= 5;
                    titreActuel.MettreAJourTitre(scoreJoueur);
                }
            }
        }
    }

    // Gère l'apparition aléatoire de mauvaise herbe
    private void GererMauvaiseHerbeAleatoire()
    {
        foreach (var terrain in terrains)
        {
            for (int i = 0; i < terrain.Plantes.Length; i++)
            {
                if (terrain.EstCaseVide(i) && !terrain.CasesMauvaiseHerbe[i])
                {
                    if (random.Next(5) == 0) // 
                    {
                        terrain.AjouterMauvaiseHerbe(i);
                        Affichage.AfficherMessage($"De la mauvaise herbe est apparue sur le terrain {Array.IndexOf(terrains, terrain) + 1}, case {i + 1} !");
                    }
                }
            }
        }
    }

    // Affiche le récapitulatif du tour
    private void AfficherRecapitulatifTour()
    {
        Affichage.AfficherRecapitulatifTour(plantesRecolteesTotal, plantesMortesTotal, scoreJoueur);
        titreActuel.AfficherMessageSiNouveauTitre(); // Affiche le message de félicitations si un nouveau titre est obtenu
    }
}