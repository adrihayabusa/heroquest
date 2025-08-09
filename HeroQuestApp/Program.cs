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
    List<Player> players = [];

    for (int i = 1; i <= playersAmount; i++) {
        string playerNumber = i switch {
            1 => "First", 
            2 => "Second", 
            3 => "Third", 
            4 => "Fourth",
            _ => "TG"
        };

        Array list = Enum.GetValues(typeof(Heroes));
        string[] names = Enum.GetNames(typeof(Heroes));
        string heroesList = string.Join(", ", names);

        WriteLine();
        Write($"{playerNumber} player's name: ");
        string playerName = ReadLine() ?? throw new Exception("Put a name!");
        WriteLine();

        Write($"Choose a class ({heroesList} | 0 - 3): ");
        string? classInput = ReadLine();
        WriteLine();

        bool valid = uint.TryParse(classInput, out uint choice);
        Heroes heroClass;
        if (valid && choice <= 3) {
            #pragma warning disable CS8605 // Unboxing a possibly null value.
            heroClass = (Heroes)list.GetValue(choice);
            #pragma warning restore CS8605 // Unboxing a possibly null value.
            WriteLine($"{playerName} choose {heroClass}.");
        } else {
            throw new Exception("All Wrong.");
        }

        Write("Hero's name: ");
        string? heroName = ReadLine();
        if (heroName == null || heroName == "") {
            heroName = "Hero" + i.ToString();
        }

        Player player = new(playerName);
        player.BuildHero(heroClass, heroName);
        players.Add(player);
    }

    WriteLine();

    string json = File.ReadAllText("quest.json");
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
