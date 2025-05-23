// Classe pour l'affichage dans la console
public class Affichage
{
    // Affiche un message de bienvenue au début du jeu
    public void AfficherMessageBienvenue()
    {
        Console.WriteLine(@"
        ╔══════════════════════════════════════╗
        ║      🧑‍🌾 Jardin Potager Virtuel     ║
        ╚══════════════════════════════════════╝");

        Console.WriteLine("Bienvenue, jardinier en herbe !");
        Console.WriteLine("Ici, vous allez apprendre à planter, entretenir, et récolter vos légumes.");
        Console.WriteLine("Mais attention aux caprices du climat et aux nuisibles !");
        Console.WriteLine();
        Console.WriteLine("Appuyez sur Entrée pour mettre les mains dans la terre 🌱");
        Console.ReadLine();
        Console.Clear();

    }

    // Affiche l'état général du jardin
    public void AfficherEtatJardin(int numeroTour, TypeSaison saison, TypeClimat meteo, string titreJoueur, Terrain[] terrains)
    {
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"🌤️  Saison : {saison}  |  ⌛ Tours : {numeroTour}  |  🌡️ Météo : {ObtenirNomClimat(meteo)}  |  🎀 Titre : {titreJoueur}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

        // Entêtes des terrains
        Console.WriteLine($"=== Terrain 1 ({terrains[0].Type}) ===      === Terrain 2 ({terrains[1].Type}) ===      === Terrain 3 ({terrains[2].Type}) ===");

        // Affichage ligne par ligne pour les 3 terrains
        for (int ligne = 0; ligne < 3; ligne++)
        {
            string ligneAffichage = "     ";
            for (int t = 0; t < terrains.Length; t++)
            {
                ligneAffichage += "[";
                for (int col = 0; col < 3; col++)
                {
                    int index = ligne * 3 + col;
                    if (terrains[t].Plantes[index] != null)
                    {
                        ligneAffichage += terrains[t].Plantes[index].ObtenirIcone();
                    }
                    else if (terrains[t].CasesMauvaiseHerbe[index])
                    {
                        ligneAffichage += "❌"; // Mauvaise herbe
                    }
                    else
                    {
                        ligneAffichage += "  "; // Case vide
                    }
                    if (col < 2) ligneAffichage += "] [";
                }
                ligneAffichage += "]";
                if (t < terrains.Length - 1) ligneAffichage += "                   "; // Espacement entre les terrains
            }
            Console.WriteLine(ligneAffichage);
        }
        Console.WriteLine();
    }

    // Retourne une chaîne de caractères pour le climat pour que ce soit plus clair dans l'affichage
    private string ObtenirNomClimat(TypeClimat climat)
    {
        return climat switch
        {
            TypeClimat.Avantageux => "Avantageux",
            TypeClimat.Neutre => "Neutre",
            TypeClimat.Desavantageux => "Désavantageux",
            TypeClimat.Tempete => "TEMPETE",
            TypeClimat.Canicule => "CANICULE",
            TypeClimat.Gel => "GEL",
        };
    }


    // Affiche le menu principal des actions pouvant être réalisées par le joueur
    public void AfficherMenuPrincipal(int actionsRestantes)
    {
        Console.WriteLine("Bienvenue dans ton jardin ! Que veux-tu faire ?");
        Console.WriteLine($"🎮 Actions restantes ce tour : {actionsRestantes}");
        Console.WriteLine("1. Planter une plante");
        Console.WriteLine("2. Arroser mes plantes");
        Console.WriteLine("3. Désherber");
        Console.WriteLine("4. Récolter");
        Console.WriteLine("5. Traiter contre les maladies");
        Console.WriteLine("6. Installer une infrastructure");
        Console.WriteLine("7. Quitter");
        Console.Write("Votre choix : ");
    }

    // Affiche le menu permettant au joueur de sélectionner le terrain
    public void AfficherSelectionTerrain()
    {
        Console.WriteLine("Choisissez le terrain où vous voulez agir :");
        Console.WriteLine("1. Terrain 1");
        Console.WriteLine("2. Terrain 2");
        Console.WriteLine("3. Terrain 3");
        Console.WriteLine("4. Retour au menu");
        Console.Write("Votre choix : ");
    }

    // Affiche le menu des plantes que le joueur peut planter
    public void AfficherMenuPlanter()
    {
        Console.WriteLine("Quelles plantes veux-tu planter ?");
        Console.WriteLine("1. Carotte (🥕) - Sableux");
        Console.WriteLine("2. Tomate (🍅) - Terre");
        Console.WriteLine("3. Pomme de terre (🥔) - Argileux");
        Console.WriteLine("4. Pomme (🍏) - Terre");
        Console.WriteLine("5. Fraise (🍓) - Terre");
        Console.WriteLine("6. Fleur Ornementale (🌸) - Sableux");
        Console.WriteLine("7. Lavande (💜) - Sableux");
        Console.WriteLine("8. Rose (🌹) - Terre");
        Console.WriteLine("9. Tournesol (🌻) - Argileux");
        Console.WriteLine("10. Mimosa (🌺) - Sableux");
        Console.WriteLine("11. Retour au menu");
        Console.Write("Votre choix : ");
    }

    // Affiche le menu pour des infrastructures que le joueur peur installer
    public void AfficherMenuInstallerInfrastructure()
    {
        Console.WriteLine("Que veux-tu installer ?");
        Console.WriteLine("1. Serre (Protège des tempêtes)");
        Console.WriteLine("2. Barrière (Protège des nuisibles)");
        Console.WriteLine("3. Pare-soleil (Protège des canicules)");
        Console.WriteLine("4. Retour au menu");
        Console.Write("Votre choix : ");
    }

    // Affiche les cases où le joueur peut planter une plante
    public void AfficherCasesDisponibles(Terrain terrain, TypePlante typePlante, int numeroTerrain)
    {
        Console.WriteLine($"Choisissez une case vide sur le Terrain {numeroTerrain} pour planter une {typePlante} (ou '0' pour annuler) :"); Console.WriteLine("Cases disponibles :");
        for (int i = 0; i < terrain.Plantes.Length; i++)
        {
            if (terrain.Plantes[i] == null && !terrain.CasesMauvaiseHerbe[i])
            {
                Console.Write($"[{i + 1}] ");
            }
            else
            {
                Console.Write("[X] "); // Case occupée ou avec mauvaise herbe
            }
            if ((i + 1) % 3 == 0) Console.WriteLine(); // Nouvelle ligne après chaque groupe de 3 cases
        }
        Console.Write("Votre choix : ");
    }

    // Affiche les plantes pour une action spécifique (arroser, récolter, traiter)
    public void AfficherPlantesPourAction(string action, Terrain terrain, int numeroTerrain, string typeFiltrage = null)
    {
        Console.WriteLine($"Quelles plantes veux-tu {action} sur le Terrain {numeroTerrain} ? (ou '0' pour annuler)");
        bool planteTrouvee = false;
        for (int i = 0; i < terrain.Plantes.Length; i++)
        {
            Plante? plante = terrain.Plantes[i];
            bool afficher = false;
            if (plante != null)
            {
                if (typeFiltrage == null)
                {
                    afficher = true;
                }
                else if (typeFiltrage == "recolte" && plante is PlanteComestible pc && pc.EstMature())
                {
                    afficher = true;
                }
                else if (typeFiltrage == "maladie" && plante.EstMalade)
                {
                    afficher = true;
                }
            }

            if (afficher)
            {
                string etatSupplementaire = "";
                if (action == "récolter" && plante is PlanteComestible pc2 && pc2.EstMature())
                {
                    etatSupplementaire = "(Prête à récolter)";
                }
                if (plante.EstMalade)
                {
                    etatSupplementaire += " (Infectée 🦠)";
                }

                Console.WriteLine($"{i + 1}. {plante.ObtenirIcone()} {plante.Nom} n°{plante.Id} - ❤️  {plante.Sante}% - ⏳{plante.ToursRestantsAvantMaturite} tours {etatSupplementaire}");
                planteTrouvee = true;
            }
        }

        if (!planteTrouvee)
        {
            Console.WriteLine($"Aucune plante disponible pour {action} sur ce terrain.");
        }
        Console.Write("Votre choix : ");
    }


    // Affiche les cases avec mauvaise herbe
    public void AfficherCasesMauvaiseHerbe(Terrain terrain, int numeroTerrain)
    {
        Console.WriteLine($"Quelles cases veux-tu désherber sur le Terrain {numeroTerrain} ? (ou '0' pour annuler)");
        bool mauvaiseHerbeTrouvee = false;
        for (int i = 0; i < terrain.Plantes.Length; i++)
        {
            if (terrain.CasesMauvaiseHerbe[i])
            {
                Console.WriteLine($"{i + 1}. Case {i + 1} (❌ Mauvaise herbe)");
                mauvaiseHerbeTrouvee = true;
            }
        }
        if (!mauvaiseHerbeTrouvee)
        {
            Console.WriteLine("Aucune mauvaise herbe sur ce terrain.");
        }
        Console.Write("Votre choix : ");
    }

    // Affiche l'état détaillé des plantes pour tous les terrains
    public void AfficherEtatDetaillePlantes(Terrain[] terrains)
    {
        Console.Clear();
        Console.WriteLine("--- État détaillé des plantes ---");
        for (int t = 0; t < terrains.Length; t++)
        {
            Console.WriteLine($"=== Terrain {t + 1} ({terrains[t].Type}) ===");
            bool plantesPresentes = false;
            for (int i = 0; i < terrains[t].Plantes.Length; i++)
            {
                Plante plante = terrains[t].Plantes[i];
                if (plante != null)
                {
                    Console.Write($"{plante.ObtenirIcone()} {plante.Nom} n°{plante.Id} - ❤️{plante.Sante}% - ");
                    if (plante.EstMature() && plante is PlanteComestible pc)
                    {
                        Console.Write($"🎉 Mature ({pc.ToursDepuisMaturation}/{pc.DureeVieApresMaturation} tours écoulés) ");
                    }
                    else if (plante is PlanteNonComestible pnc)
                    {
                        Console.Write($"⏳{pnc.ToursRestantsAvantMaturite} tours (cycle {pnc.ToursEcoules}/{pnc.DureeVieMaximale} tours) ");
                    }
                    else
                    {
                        Console.Write($"⏳{plante.ToursRestantsAvantMaturite} tours ");
                    }

                    if (plante.EstMalade)
                    {
                        Console.Write("🦠 Malade ");
                    }
                    Console.WriteLine();
                    plantesPresentes = true;
                }
            }
            if (!plantesPresentes)
            {
                Console.WriteLine("Aucune plante sur ce terrain.");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------------------------");
    }

    // Affiche le menu d'urgence pour les nuisibles
    public void AfficherMenuUrgenceNuisibles()
    {
        Console.WriteLine("1. Faire du bruit pour éloigner les nuisibles (vous serez épuisé et ne pourrez pas agir pendant 5 tours)");
        Console.WriteLine("2. Ne rien faire (il y a 1 chance sur 2 de perdre 1 à 5 plantes)");
        Console.Write("Votre choix : ");
    }

    // Affiche un message donné
    public void AfficherMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Demande une entrée à l'utilisateur et attend une touche
    public void DemanderEntreeUtilisateur(string message)
    {
        Console.WriteLine(message);
        Console.ReadLine();
    }

    // Nettoie la console
    public void NettoyerConsole()
    {
        Console.Clear();
    }

    // Affiche le récapitulatif de fin de tour
    public void AfficherRecapitulatifTour(int recoltes, int morts, int score)
    {
        Console.WriteLine("🎉 Fin du tour !");
        Console.WriteLine($"🌾 Plantes récoltées depuis le début du jeu : {recoltes} → +{recoltes * 10} points");
        Console.WriteLine($"☠️ Plantes mortes depuis le début du jeu : {morts} → -{morts * 5} points");
        Console.WriteLine($"🔢 Score final : {score} points");
    }
}