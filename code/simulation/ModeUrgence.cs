class ModeUrgence : ModeClassique
{
    private Jardin jardin {get; set;};
    public ModeUrgence(Jardin jardin)
    {
        this.jardin = jardin;
    }
}