public class Infrastructure
{
    public TypeEvenement TypeProtection { get; private set; } // Type d'événement contre lequel l'infrastructure protège
    public int ToursRestantsProtection { get; private set; } // Nombre de tours restants pour la protection

    // Constructeur de l'infrastructure
    public Infrastructure(TypeEvenement typeProtection)
    {
        TypeProtection = typeProtection;
        ToursRestantsProtection = 5; // Protection pour 5 tours
    }

    // Diminue la durée de protection restante
    public void DiminuerProtection()
    {
        if (ToursRestantsProtection > 0)
        {
            ToursRestantsProtection--;
        }
    }

    // Vérifie si l'infrastructure est encore active
    public bool EstActive()
    {
        return ToursRestantsProtection > 0;
    }
}
