public class Chips
{
    private int Amount = 1000; // Startkapital för en spelare att starta med

    public int Balance => Amount;

    public bool AddChips(int amount) // Metod för att lägga till chips, läggs till om beloppet är större än 0
    {
        if (amount > 0)
        {
            Amount += amount;
            return true;
        }

        return false;
    }

    public bool RemoveChips(int amount) // Metod för att ta bort chips, om beloppet är giltigt och saldot räcker
    {
        if (amount > 0 && Amount >= amount)
        {
            Amount -= amount;
            return true;
        }
        return false;
    }

    public void SaveChips(int amount) // Uppdaterar saldot med ett giltigt värde
{
    if (amount >= 0)
    {
        Amount = amount;
    }
}
}