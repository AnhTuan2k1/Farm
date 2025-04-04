
public class LandPlot
{
    public bool IsOccupied { get; private set; } = false;
    public FarmEntity Occupant { get; private set; }

    public void AssignEntity(FarmEntity entity)
    {
        if (IsOccupied) return;
        Occupant = entity;
        IsOccupied = true;
    }

    public void Clear()
    {
        Occupant = null;
        IsOccupied = false;
    }
}
