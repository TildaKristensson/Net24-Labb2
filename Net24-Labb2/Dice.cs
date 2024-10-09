
class Dice
{
    private readonly int numberOfDice;
    private readonly int sidesPerDice;
    private readonly int modifier;

    private readonly Random random = new();

    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this.numberOfDice = numberOfDice;
        this.sidesPerDice = sidesPerDice;
        this.modifier = modifier;
    }

    public int RollDice()
    {
        var diceSum = 0;
        for (int i = 0; i < numberOfDice; i++) 
        {
            diceSum += random.Next(1, sidesPerDice + 1); 
        }

        return diceSum + modifier;
    }

    public string GetDiceStats()
    {
        return $"{numberOfDice}d{sidesPerDice}+{modifier}";
    }
}
