namespace Libraries;

public abstract class Objective
{
    public Objectives Name { get; set; }

    public bool IsCompleted { get; protected set; }

    public abstract void CheckCompletion();

    public abstract void OnCompletion();
}
