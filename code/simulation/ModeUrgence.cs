class ModeUrgence : ModeClassique
{
    private Jardin jardin { get; set; };

    // Constructeur de ModeUrgence
    public ModeUrgence(Jardin jardin)
    {
        this.jardin = jardin;
    }
}