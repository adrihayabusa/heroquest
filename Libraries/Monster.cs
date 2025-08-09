using Libraries.Enum;
using Libraries.Shared;

namespace Libraries;

public class Monster: Entity
{
    public uint Id;

    public Monsters Type;

    public Weapon Weapon = new();

    public Statistics Stats = new();
}
