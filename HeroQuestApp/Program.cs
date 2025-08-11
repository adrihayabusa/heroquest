using HeroQuestApp;
using Libraries;
using Libraries.Enum;
using Newtonsoft.Json;
using System.Text;

OutputEncoding = Encoding.UTF8;

Board board = new();
Ennemy master = new();

board.Load();
Write("Welcome to HeroQuest console! How many players? (2 - 4): ");

string playersInput = ReadLine() ?? throw new Exception("Ok bye!");
uint playersAmount = Convert.ToUInt16(playersInput);

WriteLine();

if (playersAmount < 2 || playersAmount > 4) {
    WriteLine("Wrong input.");
} else {
    List<Heroes> availableHeroes = [.. Enum.GetValues<Heroes>()];
    List<Player> players = [];

    for (int i = 1; i <= playersAmount; i++) {
        string playerNumber = i switch {
            1 => "First", 
            2 => "Second", 
            3 => "Third", 
            4 => "Fourth",
            _ => "TG"
        };

        WriteLine();
        Write($"{playerNumber} player's name: ");
        string playerName = ReadLine() ?? throw new Exception("Put a name!");
        WriteLine();

        Heroes[] heroValues = Enum.GetValues<Heroes>();
        Write($"Choose a class ({availableHeroes} | 0 - {availableHeroes.Count - 1}): ");
        string? heroClassInput = ReadLine();
        WriteLine();

        if (int.TryParse(heroClassInput, out int choice) && choice >= 0 && choice < availableHeroes.Count)
        {
            Heroes heroClass = availableHeroes[choice];
            availableHeroes.RemoveAt(choice);
            WriteLine($"{playerName} chose {heroClass}.");

            Write("Hero's name: ");
            string? heroName = ReadLine();
            if (heroName == null || heroName == "") {
                heroName = "Hero" + i.ToString();
            }

            Player player = new(playerName);
            player.BuildHero(heroClass, heroName);
            players.Add(player);
        }
    }

    WriteLine();

    var path = Path.Combine(AppContext.BaseDirectory, "quest.json");
    string json = File.ReadAllText(path);
    Quest quest = JsonConvert.DeserializeObject<Quest>(json) ??
        throw new Exception("The quest is empty. Check quest.json.");

    try {
        quest.Load(master, players, board);

        Start(board, quest);
    } catch (Exception ex) {
        WriteLine($"""
            Sorry, an error has occured. 
            Reason: {ex.Message}
            Stack trace: {ex.StackTrace}
            """);
    }
}
