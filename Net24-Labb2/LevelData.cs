
class LevelData
{
    private List<LevelElement> _elements;

    public List<LevelElement> Elements
    {
        get { return _elements; }
    }

    public void Load(string textfile)
    {
        _elements = new List<LevelElement>();

        Player? player = null;
        using (StreamReader reader = new StreamReader(textfile))
        {
            var x = 0;
            var y = 4;

            while (!reader.EndOfStream)
            {
                int readChar = reader.Read();
                char c = (char)readChar;

                switch (c)
                {
                    case '#':
                        _elements.Add(new Wall(x, y));
                        x++;
                        break;

                    case 'r':
                        _elements.Add(new Rat(x, y));
                        x++;
                        break;

                    case 's':
                        _elements.Add(new Snake(x, y));
                        x++;
                        break;

                    case '@':
                        player = new Player(x, y);
                        _elements.Add(player);
                        x++;
                        break;

                    case '\n':
                        y++;
                        x = 0;
                        break;

                    case '\r':
                        break;

                    default:
                        Console.Write(c);
                        x++;
                        break;
                }

             
            }
            
        }

        if(player == null)
        {
            return;
        }

        foreach (var element in _elements)
        {
            if(element is Enemy enemy)
            {
                enemy.SetVisibility(player);
            }
            else if (element is Wall wall)
            {
                wall.SetVisibility(player);
            }

            element.Draw();
        }
    }
}
    
