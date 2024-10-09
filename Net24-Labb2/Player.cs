

class Player : LevelElement
{
    public Player(int x, int y) : base(x, y, '@', ConsoleColor.Cyan)
    {
        Health = 100;
        Name = "Player";
        AttackDice = new(2, 6, 2);
        DefenceDice = new(2, 6, 0);
    }

    public string Name { get; set; }

    public int Health { get; set; }

    public Dice AttackDice { get; set; }

    public Dice DefenceDice { get; set; }

    protected override bool IsVisible { get; set; } = true;
    public bool IsAlive { get; set; } = true;


    public void Move(ConsoleKeyInfo moveInput, List<LevelElement> elements)
    {
        var x = X;
        var y = Y;

        switch (moveInput.Key)
        {
            case ConsoleKey.RightArrow:
                x++;
                break;

            case ConsoleKey.LeftArrow:
                x--;
                break;

            case ConsoleKey.UpArrow:
                y--; 
                break;

            case ConsoleKey.DownArrow:
                y++;
                break;
        }

        foreach (var element in elements)
        {
            if (element.X == x && element.Y == y)
            {
                if(element is Enemy enemy)
                {
                    if (enemy.IsAlive)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.Write(new string(' ', Console.WindowWidth));

                        Console.SetCursorPosition(0, 2);
                        Console.Write(new string(' ', Console.WindowWidth));

                        Console.SetCursorPosition(0, 1);
                        Attack(enemy);
                        if (enemy.IsAlive)
                            enemy.Attack(this);

                        return;
                    }
                    break;
                }
                return;
            }
        }


        Console.SetCursorPosition(X, Y);
        Console.Write(" ");

        X = x; 
        Y = y;
    }

    public void Attack(Enemy enemy)
    {
        var playerAttackDamage = AttackDice.RollDice();
        var enemyDefenseDamage = enemy.DefenceDice.RollDice();

        var damage = playerAttackDamage - enemyDefenseDamage;
        if(damage < 0)
            damage = 0;

        enemy.Health -= damage;

        if(enemy.Health <= 0)
        {
            enemy.IsAlive = false;
        }

        var extraMessage = "";
        if (!enemy.IsAlive)
        {
            extraMessage = "now it's unalived.";
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        else if (damage == 0)
        {
            extraMessage = "not harming it.";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (damage < 3)
        {
            extraMessage = "slightly wounding it.";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (damage < 7)
        {
            extraMessage = "moderately wounding it.";
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (damage >= 7)
        {
            extraMessage = "severely wounding it.";
            Console.ForegroundColor = ConsoleColor.Red;
        }

        var message = $"You (ATK: {AttackDice.GetDiceStats()} => {playerAttackDamage}) attacked the {enemy.GetType().Name} " +
            $"(DEF: {enemy.DefenceDice.GetDiceStats()} => {enemyDefenseDamage}), {extraMessage} Damage dealt: {damage}";

        Console.WriteLine(message);
    }
}
