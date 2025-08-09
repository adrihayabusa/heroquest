namespace Libraries;

public class Board
{
    public List<Tile> Tiles { get; set; } = [];

    public int XMax { get; set; } = 0;

    public int YMax { get; set; } = 0;

    public void LoadTest() {
        string[] lines = File.ReadAllLines("board.txt");

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char c = lines[y][x];

                switch (c) {
                    case '0':
                        Write("."); // Case vide
                        break;
                    case '1':
                        Write("_"); // Mur en bas
                        break;
                    case '2':
                        Write("|"); // Mur à droite
                        break;
                    case '3':
                        Write("^"); // Mur en haut
                        break;
                    case '4':
                        Write("<"); // Mur à gauche
                        break;
                    case '5':
                        Write("L"); // Mur en bas et à droite
                        break;
                    case '6':
                        Write("7"); // Mur à droite et en haut
                        break;
                    case '7':
                        Write("J"); // Mur en haut et à gauche
                        break;
                    case '8':
                        Write("T"); // Mur en bas et en haut
                        break;
                    case '9':
                        Write("+"); // Mur en haut et en bas
                        break;
                    case 'A':
                        Write("H"); // Mur à gauche et à droite
                        break;
                    case 'B':
                        Write("#"); // Murs partout
                        break;
                    default:
                        Write(" "); // Caractère inconnu
                        break;
                }
            }
            WriteLine();
        }
    }

    public void LoadTest2() {
        string[] lines = File.ReadAllLines(".txt");

        YMax = lines.Length;
        XMax = lines[YMax -1].Length;

        for (int y = 0; y < YMax; y++) {
            for (int x = 0; x < XMax; x++) {
                char c = lines[y][x];

                switch (c) {
                    case '0':
                        Write("."); // Case vide
                        break;
                    case '1':
                        Write("□"); // Mur
                        break;
                    default:
                        Write(" "); // Caractère inconnu
                        break;
                }
            }
            WriteLine();
        }
    }

    public void Load() {
        var path = Path.Combine(AppContext.BaseDirectory, "board2.txt");
        string[] lines = File.ReadAllLines(path);

        YMax = lines.Length;
        XMax = lines[YMax -1].Length;

        for (int y = 0; y < YMax; y++) {
            for (int x = 0; x < lines[y].Length; x++) {
                char c = lines[y][x];
                
                uint tileValue = Convert.ToUInt16(c.ToString(), 16);
                Tile tile = new((uint)x, (uint)y) {
                    HasWall = tileValue == 1,
                    Walls = (Walls)tileValue
                };
                SetTile((uint)x, (uint)y, tile);
            }
        }
    }

    public Tile? GetTile(uint x, uint y) {
        return Tiles.Find(tile => tile.GetX() == x && tile.GetY() == y);
    }

    public void SetTile(uint x, uint y, Tile tile) {
        Tile? existingTile = Tiles.Find(t => t.GetX() == x && t.GetY() == y);

        if (existingTile != null) {
            int index = Tiles.IndexOf(existingTile);
            Tiles[index] = tile;
        } else {
            Tiles.Add(tile);
        }
    }
}
