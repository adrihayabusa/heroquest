using Libraries.Shared;
using Libraries.Enum;

namespace Libraries;

public class Hero: Entity
{
    public string Name { get; set; } = "hero";

    public Heroes Class { get; set; }

    public Statistics Stats { get; set; } = new();

    public Weapon Weapon { get; set; } = new();

    public Armor Armor { get; set; } = new();

    public bool CanUseMagic { get; set; } = false;

    public List<Spells>? Spells { get; set; }

    public bool HasCollapsed = false;
}
