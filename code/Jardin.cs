abstract class Jardin
{
    public int NbTerrain { get; }
    public int Ligne { get; }
    public int Colonne { get; }
    public List<TerrainBase> Terrains { get; }

    public Jardin(int nbTerrain, int ligne, int colonne)
    {
        NbTerrain = nbTerrain;
        Ligne = ligne;
        Colonne = colonne;
    }
}