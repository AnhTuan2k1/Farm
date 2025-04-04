using System;

public abstract class FarmEntity
{
    public FarmEntityConfig Config { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public bool IsHarvested { get; protected set; } = false;

    public bool IsDead(DateTime now)
    {
        float elapsed = (float)(now - CreatedAt).TotalSeconds;
        return elapsed >= Config.HarvestIntervalSeconds * Config.MaxYield + Config.LifetimeSeconds;
    }

    public bool CanHarvest(DateTime now)
    {
        if (IsHarvested || IsDead(now)) return false;
        float elapsed = (float)(now - CreatedAt).TotalSeconds;
        return elapsed >= Config.HarvestIntervalSeconds * Config.MaxYield;
    }

    public abstract int Harvest(DateTime now);
}
