namespace Libraries;

public enum Walls
{
    None = 0,
    Bottom = 1,
    Right = 2,
    Top = 3,
    Left = 4,
    BottomRight = 5,
    TopRight = 6,
    TopLeft = 7,
    BottomLeft = 8,
    TopBottom = 9,
    LeftRight = 10,
    All = 11
}

public enum DoorDirections
{
    None,
    TopBottom,
    LeftRight,
    Other
}

public class Tile(uint x, uint y)
{
    private uint X { get; set; } = x;

    private uint Y { get; set; } = y;

    public bool HasWall { get; set; } = false;

    public bool HasDoor { get; set; } = false;

    public bool HasTrap { get; set; } = false;

    public Walls Walls { get; set; }

    public DoorDirections Doors { get; set; }

    public Traps Trap { get; set; }

    public bool IsStair { get; set; } = false;

    public Room Room { get; set; } = new();

    public Hero? Hero { get; set; }

    public Monster? Monster { get; set; }

    public uint GetX() {
        return X;
    }

    public void SetX(uint x = 0) => X = x;

    public uint GetY() {
        return Y;
    }

    public void SetY(uint y = 0) => Y = y;
}
