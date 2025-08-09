using Libraries;

namespace HeroQuestApp;

public class Quest
{
    public string Description { get; set; } = "";

    public string? Items { get; set; }

    public List<Trap>? Traps { get; set; }

    public List<Monster> Monsters { get; set; } = [];

    public List<Player> Players { get; set; } = [];

    public List<Door>? Doors { get; set; }

    public Position Stairs { get; set; } = new(0, 0);

    public List<Position> PlayersPositions { get; set; } = [];

    public List<Objectives> ObjectivesDescriptions { get; set; } = [];

    public List<Objective> Objectives { get; set; } = [];

    public bool IsCompleted = false;

    public void Load(Ennemy ennemy, List<Player> players, Board board) {
        Players = players;

        if (Monsters != null) {
            foreach (Monster monster in Monsters) {
                ennemy.BuildMonster(monster);

                Tile tile = board.GetTile(monster.Position.X, monster.Position.Y) ?? throw new ArgumentException(
                    $"No Tile found at x:{monster.Position.X}, y:{monster.Position.Y}"
                );
                tile.Monster = monster;
            }
        }

        if (Doors != null) {
            foreach (Door door in Doors) {
                Tile tile = board.GetTile(door.Position.X, door.Position.Y) ?? throw new ArgumentException(
                    $"No Tile found at x:{door.Position.X}, y:{door.Position.Y}"
                );
                tile.HasWall = false;
                tile.HasDoor = true;
                tile.Doors = door.Directions;

                board.SetTile(door.Position.X, door.Position.Y, tile);
            }
        }

        if (Traps != null) {
            foreach (Trap trap in Traps) {
                Tile tile = board.GetTile(trap.Position.X, trap.Position.Y) ?? throw new ArgumentException(
                    $"No Tile found at x:{trap.Position.X}, y:{trap.Position.Y}"
                );
                tile.HasWall = false;
                tile.HasTrap = true;
                tile.Trap = trap.Type;

                board.SetTile(trap.Position.X, trap.Position.Y, tile);
            }
        }

        if (Stairs != null) {
            Tile tile = board.GetTile(Stairs.X, Stairs.Y) ?? throw new ArgumentException(
                    $"No Tile found at x:{Stairs.X}, y:{Stairs.Y}"
                );
            tile.IsStair = true;
            tile.HasWall = false;

            board.SetTile(Stairs.X, Stairs.Y, tile);
        }

        foreach (Objectives objectiveDesc in ObjectivesDescriptions) {
            Objective objective = objectiveDesc switch {
                Libraries.Objectives.KillAllMonsters => new KillAllMonstersObjective(board),
                _ => new(board)
            };

            Objectives.Add(objective);
        }

        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++) {
                players[playerIndex].Hero.Position.X = PlayersPositions[playerIndex].X;
                players[playerIndex].Hero.Position.Y = PlayersPositions[playerIndex].Y;

                Tile tile = board.GetTile(
                    players[playerIndex].Hero.Position.X,
                    players[playerIndex].Hero.Position.Y) ?? throw new ArgumentException(
                    $"No Tile found at x:{players[playerIndex].Hero.Position.X}, y:{players[playerIndex].Hero.Position.Y}"
                );

                tile.Hero = players[playerIndex].Hero;
        }
    }

    public void Beginning() {
        Clear();

        int delay = 10;
        SetCursorPosition(0, 0);

        foreach (char c in Description) {
            Write(c);

            Thread.Sleep(delay);
        }
        WriteLine("\n\nPress any key to continue.");
        ConsoleKeyInfo key = ReadKey();

        Clear();
        WriteLine($"Your team is made of {Players.Count} hero(es).");
        Table(Players);
        WriteLine("\n\nPress any key to continue.");
        key = ReadKey();
    }

    public void Table(List<Player> list) {
        List<string> playerAttributes = [
            "         |",
            "Hero:    |",
            "Class:   |",
            "Attack:  |",
            "Defense: |",
            "Body:    |",
            "Mind:    |",
            "Weapon:  |",
            "Armor:   |"
        ];

        foreach(Player player in list) {
            playerAttributes[0] += $"{player.Name,10}|";
            playerAttributes[1] += $"{player.Hero.Name,10}|";
            playerAttributes[2] += $"{player.Hero.Class,10}|";
            playerAttributes[3] += $"{player.Hero.Stats.AttackDice + player.Hero.Weapon.AttackBonusDice,10}|";
            playerAttributes[4] += $"{player.Hero.Stats.DefenseDice + player.Hero.Armor.DefenseBonusDice,10}|";
            playerAttributes[5] += $"{player.Hero.Stats.Body,10}|";
            playerAttributes[6] += $"{player.Hero.Stats.Mind,10}|";
            playerAttributes[7] += $"{player.Hero.Weapon.Name,10}|";
            playerAttributes[8] += $"{player.Hero.Armor.Name,10}|";
        }

        int dashCount = playerAttributes[0].Length;
        string line = string.Concat(Enumerable.Repeat('-', dashCount));

        foreach (string playerAttribute in playerAttributes) {
            Write(playerAttribute);
            WriteLine($"\n{line}");
        }
    }
}
