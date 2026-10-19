public class Chips
{
    private int Amount = 1000;

    public bool AmountIsPositive()
    {
        return Amount > 0;
    }

    public void AddChips(int amount)
    {
        Amount += amount;
    }

    public void RemoveChips(int amount)
    {
        Amount -= amount;
    }
}