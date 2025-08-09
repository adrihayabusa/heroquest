using HeroQuestApp;
using Libraries;

partial class Program
{

    static void Start(Board board, Quest quest) {
        bool gameOver = false;
        int outputY = board.YMax +1;

        quest.Beginning();

        Display(board);
        CustomWriteLine(0, outputY, "Here is the map.");

        while (gameOver != true) {
            foreach (Player player in quest.Players) {
                var tokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = tokenSource.Token;

                // TOOD Maybe let's see that later...
/*                 Task displayTask = Task.Run(() => {
                    BlinkHero(board, player.Hero);
                }, cancellationToken); */

                HighlightHero(board, player.Hero);

                Task gameTask = Task.Run(() => {
                    CustomWriteLine(0, outputY +1, $"{player.Name} - Choose an option:");

                    string[] options = ["Move", "Action"];
                    int index = 0;
                    bool firstOption = false;
                    ConsoleKeyInfo keyinfo;

                    do {
                        for (int i = 0; i < options.Length; i++) {
                            SetCursorPosition(0, outputY + 2 + i);
                            int spaces = WindowWidth * (WindowHeight - outputY);

                            //Write(new string(' ', spaces));
                            
                            SetCursorPosition(0, outputY + 2 + i);
                            if (i == index) {
                                BackgroundColor = ConsoleColor.White;
                                ForegroundColor = ConsoleColor.Black;
                            } else {
                                ResetColor();
                            }

                            WriteLine($"{i + 1}. {options[i]}");
                        }
                        keyinfo = ReadKey();

                        if (keyinfo.Key == ConsoleKey.DownArrow) {
                            if (index < options.Length - 1) {
                                index++;
                            }
                        }

                        if (keyinfo.Key == ConsoleKey.UpArrow) {
                            if (index > 0) {
                                index--;
                            }
                        }

                        if (keyinfo.Key == ConsoleKey.Enter) {
                            firstOption = true;
                            ResetColor();

                            switch (index) {
                                case 0:
                                    int playerMoves = player.Moves();
                                    CustomWriteLine(0, outputY, $"You chose to move. Where? (Move = {playerMoves})");

                                    MoveHero(player, board, playerMoves);

                                    break;
                                case 1:
                                    CustomWriteLine(0, outputY, "You chose to perform an action.");
                                    break;
                            }
                        }
                    }
                    while (firstOption != true);

                    //tokenSource.Cancel();
                    HighlightHero(board, player.Hero, true);

                    return;
                });

                Task.WaitAny(gameTask);
            }

            //gameOver = true;
        }
    }

    static void CustomWriteLine(int outputX, int outputY, string? message) {
        SetCursorPosition(outputX, outputY);
        int spaces = WindowWidth * (WindowHeight - outputY);

        Write(new string(' ', spaces));
        
        SetCursorPosition(outputX, outputY);
        WriteLine(message);
    }

    static void Display(Board board) {
        int outputX = (WindowWidth - board.XMax) / 2;
        int outputY = board.YMax + 1;

        SetCursorPosition(outputX, 0);
        Clear();
        DisplayBoard(board, board.XMax, board.YMax);
        SetCursorPosition(outputX, outputY);
    }

    static void DisplayBoard(Board board, int boardWidth, int boardHeight) {
        int outputX = (WindowWidth - boardWidth) / 2;

        for (uint y = 0; y < boardHeight; y++) {
            SetCursorPosition(outputX, (int)y);

            for (uint x = 0; x < boardWidth; x++) {
                Tile tile = board.GetTile(x, y) ?? throw new ArgumentException($"No tile found for x:{x}, y:{y}");

                switch (tile) {
                    case { HasWall: true }:
                        Write("□");
                        break;
                    case { HasDoor: true }:
                        if (tile.Doors == DoorDirections.LeftRight) {
                            Write("|");
                        } else {
                            Write("-");
                        }
                        break;
                    case { HasTrap: true }:
                        Write("x");
                        break;
                    case { IsStair: true }:
                        Write("$");
                        break;
                    case { Hero: { } }:
                        Write(tile.Hero.Class.ToString()[0]);
                        break;
                    case { Monster: { } }:
                        Write(tile.Monster.Type.ToString()[0]);
                        break;
                    default:
                        Write(".");
                        break;
                }
            }
            WriteLine();
        }
    }

    static void HighlightHero(Board board, Hero hero, bool reset = false) {
        int heroX = (int)hero.Position.X;
        int heroY = (int)hero.Position.Y;
        int centerX = (WindowWidth - board.XMax) / 2;
        char heroChar = hero.Class.ToString()[0];

        SetCursorPosition(centerX + heroX, heroY);

        ForegroundColor = hero.Class switch {
            Libraries.Enum.Heroes.Barbarian => ConsoleColor.Red,
            Libraries.Enum.Heroes.Dwarf => ConsoleColor.Green,
            Libraries.Enum.Heroes.Elf => ConsoleColor.Cyan,
            Libraries.Enum.Heroes.Wizard => ConsoleColor.DarkBlue,
            _ => ConsoleColor.White
        };
        if (reset) {
            ForegroundColor = ConsoleColor.White;
        }
        Write(heroChar);

        ResetColor();

        SetCursorPosition(0, board.YMax +1);
    }

    static void MoveHero(Player player, Board board, int moves) {
        uint heroX = player.Hero.Position.X;
        uint heroY = player.Hero.Position.Y;
        int centerX = (WindowWidth - board.XMax) / 2;

        Tile ?tile = board.GetTile(heroX, heroY) ?? throw new Exception("No tile.");
        tile.Hero = null;
        board.SetTile(heroX, heroY, tile);

        while (moves != 0) {
            if (IsMovePossible(ref heroX, ref heroY, board)) {
                SetCursorPosition(centerX + (int) heroX, (int) heroY);

                BackgroundColor = ConsoleColor.Green;
                Write(' ');
                ResetColor();
                
                SetCursorPosition(centerX + (int) heroX, (int) heroY);

                moves--;
            }
        }

        player.Hero.Position.X = heroX;
        player.Hero.Position.Y = heroY;
        
        Tile ?tileWithHero = board.GetTile(heroX, heroY) ?? throw new Exception("No tile.");
        tileWithHero.Hero = player.Hero;
        board.SetTile(heroX, heroY, tileWithHero);

        Display(board);
    }

    static bool IsMovePossible(ref uint x, ref uint y, Board board)
    {
        ConsoleKeyInfo keyPosition = ReadKey();
        uint initialX = x;
        uint initialY = y;

        switch (keyPosition.Key) {
            case ConsoleKey.UpArrow:
                y--;
            break;
            case ConsoleKey.DownArrow:
                y++;
            break;
            case ConsoleKey.LeftArrow:
                x--;
            break;
            case ConsoleKey.RightArrow:
                x++;
            break;
            default:
                return false;
        }

        Tile tile = board.GetTile(x, y) ?? throw new Exception("No tile!");

        if (tile.HasWall || tile.Monster != null || tile.Hero != null) {
            x = initialX;
            y = initialY;

            return false;
        }
        return true;
    }

    static void BlinkHero(Board board, Hero hero) {
        int heroX = (int)hero.Position.X;
        int heroY = (int)hero.Position.Y;
        int centerX = (WindowWidth - board.XMax) / 2;
        char heroChar = hero.Class.ToString()[0];

        while (true) {
            SetCursorPosition(centerX + heroX, heroY);
            Write(heroChar);

            Thread.Sleep(500);

            SetCursorPosition(centerX + heroX, heroY);
            Write(".");

            Thread.Sleep(500);
        }
    }

    static void UpdateBoard() {

    }
}
