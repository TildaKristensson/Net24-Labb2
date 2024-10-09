
using Net24_Labb2.Interfaces;
using System.Reflection.Emit;

class Snake : Enemy, IVisible
{

    public Snake(int x, int y) : base(x, y, 's', ConsoleColor.Green)
    {
        Health = 25; 
        Name = "Snake";
        AttackDice = new(3, 4, 2);
        DefenceDice = new(1, 8, 5);
    }

    private bool SnakeShouldFlee(Player player)
    {
        if (Math.Abs(X -  player.X) <= 1 && Math.Abs(Y - player.Y) <= 1)
        {
            return true;
        }

        return false;
    }

    public override void Update(List<LevelElement> elements, Player player)
    {
        ShouldDraw = IsAlive;
        if (!IsAlive)
            return;

        if (!SnakeShouldFlee(player))
        {
            return;
        }

        var x = X;
        var y = Y;

        var deltaX = X - player.X;
        var deltaY = Y - player.Y;


        if (deltaX < 0)
        {
            x--;
        }
        else if (deltaX > 0)
        {
            x++;
        }
        else if (deltaY < 0)
        {
            y--;
        }
        else if (deltaY > 0)
        {
            y++;
        }

        foreach (var element in elements)
        {
            if (element.X == x && element.Y == y)
            {
                return;
            }
        }

        Console.SetCursorPosition(X, Y);
        Console.Write(" ");

        X = x;
        Y = y;
    }
}
