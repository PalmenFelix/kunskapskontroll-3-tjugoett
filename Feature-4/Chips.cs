public class Chips
{
    private int Amount = 1000;

    public int Balance => Amount;

    public bool AddChips(int amount)
    {
        if (amount > 0)
        {
            Amount += amount;
            return true;
        }

        return false;
    }

    public bool RemoveChips(int amount)
    {
        if (amount > 0 && Amount >= amount)
        {
            Amount -= amount;
            return true;
        }
        return false;
    }
}