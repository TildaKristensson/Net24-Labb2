
using Net24_Labb2.Interfaces;
using System;
using System.Net.Http.Headers;

abstract class Enemy : LevelElement, IVisible
{
    protected Enemy(int x, int y, char objectIcon, ConsoleColor charColor) : base(x, y, objectIcon, charColor)
    {

    }

    public string Name { get; set; }

    public int Health { get; set; }

    public Dice AttackDice { get; set; }

    public Dice DefenceDice { get; set; }
    public bool IsAlive { get; set; } = true;

    public void SetVisibility(Player player)
    {
        var distance = Math.Sqrt(Math.Pow(player.X - X, 2) + Math.Pow(player.Y - Y, 2));
        if(distance <= 5 && IsAlive)
        {
            IsVisible = true;
        }
        else
        {
            IsVisible = false;
        }
    }

    public abstract void Update(List<LevelElement> elements, Player player);

    public void Attack(Player player)
    {
        var enemyAttackDamage = AttackDice.RollDice();
        var playerDefenseDamage = player.DefenceDice.RollDice();

        var damage = enemyAttackDamage - playerDefenseDamage;
        if (damage < 0)
        {
            damage = 0;
        }

        player.Health -= damage;

        if (player.Health <= 0)
        {
            player.IsAlive = false;
        }

        var extraMessage = "";
        if (!player.IsAlive)
        {
            extraMessage = "you are unalived and the game is over.";
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        else if (damage == 0)
        {
            extraMessage = "not harming you.";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (damage < 3)
        {
            extraMessage = "slightly wounding you.";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (damage < 7)
        {
            extraMessage = "moderately wounding you.";
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (damage >= 7)
        {
            extraMessage = "severely wounding you.";
            Console.ForegroundColor = ConsoleColor.Red;
        }

        var message = $"The {GetType().Name} (ATK: {AttackDice.GetDiceStats()} => {enemyAttackDamage}) attacked you " +
            $"(DEF: {player.DefenceDice.GetDiceStats()} => {playerDefenseDamage}), {extraMessage} Damage dealt: {damage}";

        Console.WriteLine(message);
    }
}
