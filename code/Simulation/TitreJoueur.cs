public class TitreJoueur
{
    // Titre actuel du joueur
    public string NomTitre { get; private set; }

    // Score auquel le titre actuel a été obtenu
    private int scoreAtteintPourTitre;

    // Affichage pour afficher les messages de félicitations
    private Affichage Affichage;

    // Constructeur de TitreJoueur
    public TitreJoueur()
    {
        NomTitre = "🐣 Novice"; // Titre au début du jeu
        scoreAtteintPourTitre = 0;
        Affichage = new Affichage();
    }

    // Met à jour le titre du joueur en fonction du score
    // Un titre obtenu ne peut pas être perdu même si le joueur repasse en dessous de la barre nécessaire pour l'obtenir
    public void MettreAJourTitre(int scoreActuel)
    {
        if (scoreActuel >= 100 && scoreAtteintPourTitre < 100)
        {
            NomTitre = "🥇 Maître du potager";
            scoreAtteintPourTitre = 100;
        }
        else if (scoreActuel >= 50 && scoreAtteintPourTitre < 50)
        {
            NomTitre = "🌱 Jeune pousse";
            scoreAtteintPourTitre = 50;
        }
    }

    // Affiche un message de félicitations si un nouveau titre a été obtenu au tour précédent
    public void AfficherMessageSiNouveauTitre()
    {
        if (scoreAtteintPourTitre == 50 && NomTitre == "🌱 Jeune pousse")
        {
            Affichage.AfficherMessage("🎉 Félicitations ! Tu as atteint 50 points, tu as gagné le titre de jeune pousse !");
        }
        else if (scoreAtteintPourTitre == 100 && NomTitre == "🥇 Maître du potager")
        {
            Affichage.AfficherMessage("🎉 Félicitations ! Tu as atteint 100 points, tu as gagné le titre de maître du potager !");
        }
    }
}
