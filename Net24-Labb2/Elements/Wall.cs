

using Net24_Labb2.Interfaces;

class Wall : LevelElement, IVisible
{

    public Wall(int x, int y) : base(x, y, '#', ConsoleColor.Gray) 
    {
      
    }

    public void SetVisibility(Player player)
    {
        if (IsVisible)
        {
            ShouldDraw = false;
            return;
        }

        var distance = Math.Sqrt(Math.Pow(player.X - X, 2) + Math.Pow(player.Y - Y, 2));
        if (distance <= 5)
        {
            IsVisible = true;
        }
    }
}
