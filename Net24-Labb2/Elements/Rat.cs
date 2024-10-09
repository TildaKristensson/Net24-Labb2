
using Net24_Labb2.Interfaces;
using System.Data;


class Rat : Enemy
{ 
    private Random Random = new();

    public Rat(int x, int y) : base(x, y, 'r', ConsoleColor.Red)
    {
        Health = 10; 
        Name = "Rat";
        AttackDice = new(1, 6, 3);
        DefenceDice = new(1, 6, 1);
    }

    public override void Update(List<LevelElement> elements, Player player) 
    {
        ShouldDraw = IsAlive;
        if(!IsAlive)
            return;

        var x = X;
        var y = Y;

        var moveDirection = Random.Next(1, 5);

        switch (moveDirection)
        {
            case 1:
                x++;
                break;

            case 2:
                x--;
                break;

            case 3:
                y--;
                break;

            case 4:
                y++;
                break;
        }

        foreach (var element in elements)
        {
            if (element.X == x && element.Y == y)
            {
                if (element is Player)
                {
                    if (player.IsAlive)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.Write(new string(' ', Console.WindowWidth));

                        Console.SetCursorPosition(0, 2);
                        Console.Write(new string(' ', Console.WindowWidth));

                        Console.SetCursorPosition(0, 1);
                        Attack(player);
                        if (player.IsAlive)
                            player.Attack(this);

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

}
