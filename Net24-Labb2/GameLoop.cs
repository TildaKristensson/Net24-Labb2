
public class GameLoop
{
    public GameLoop()
    {

    }

    public void RunGame()
    {
        LevelData level = new LevelData();
        Console.CursorVisible = false;

        var textfile = Path.Combine("Levels", "Level1.txt");

        level.Load(textfile);

        Player? player = null;
        foreach (var element in level.Elements)
        {
            if (element is Player)
            {
                player = (Player)element;
            }
        }

        if (player == null)
        {
            return;
        }

        LoopGame(level, player);
    }

    private void LoopGame(LevelData level, Player? player)
    {
        var maxHealth = player!.Health;

        var turn = 1;
        while (true)
        {
            var moveInput = Console.ReadKey();

            CleanUpperConsole();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Name: {player.Name} - Health: {player.Health}/{maxHealth} - Turn: {turn}");

            player.Move(moveInput, level.Elements);

            foreach (var element in level.Elements)
            {
                if (element is Enemy enemy)
                {
                    enemy.Update(level.Elements, player);
                    enemy.SetVisibility(player);
                }
                if (element is Wall wall)
                {
                    wall.SetVisibility(player);
                }
                if (element.ShouldDraw)
                {
                    element.Draw();
                }
                    
            }

            turn++;

            if (player.Health <= 0)
            {
                break;
            }         
        }

    }

    private void CleanUpperConsole()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(new string(' ', Console.WindowWidth));

        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', Console.WindowWidth));

        Console.SetCursorPosition(0, 2);
        Console.Write(new string(' ', Console.WindowWidth));
    }
}



