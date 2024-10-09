
abstract class LevelElement
{
    public int X { get; set; }
    public int Y { get; set; }
    protected char Icon { get; set; }
    protected virtual bool IsVisible { get; set; } = false;
    public virtual bool ShouldDraw { get; set; } = true;

    protected ConsoleColor IconColor { get; set; }

    public LevelElement(int x, int y, char icon, ConsoleColor iconColor)
    {
        X = x;
        Y = y;
        Icon = icon;
        IconColor = iconColor;
    }


    public void Draw()
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = IconColor;
        if (IsVisible) 
        {
         
            Console.WriteLine(Icon);
        }
        else
        {
            Console.WriteLine(" ");

        }

        Console.ResetColor();
    }
}
