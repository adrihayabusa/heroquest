using Libraries.Enum;

namespace Libraries;

public class KillAllMonstersObjective(Board board) : Objective
{
    private readonly Board board = board;

    public new Objectives Name = Objectives.KillAllMonsters;

    public override void CheckCompletion()
    {
        // Loop through the board and look for monsters
        for (uint x = 0; x < board.XMax; x++)
        {
            for (uint y = 0; y < board.YMax; y++)
            {
                // Get the tile at the current position
                Tile tile = board.GetTile(x, y) ?? throw new ArgumentException($"No tile found at x:{x}, y:{y}");

                if (tile.Monster != null)
                {
                    IsCompleted = false;
                    return;
                }
            }
        }

        // If no monsters are found, the objective is completed
        IsCompleted = true;
    }

    public override void OnCompletion()
    {
        throw new NotImplementedException();
    }
}
