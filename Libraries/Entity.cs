using Libraries;

namespace Libraries.Shared;

public abstract class Entity
{
    public char Symbol { get; set; }
    
    public Position Position { get; set; } = new(0,0);

    protected void Interact(Entity entity, Board board) {}
}
