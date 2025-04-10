using Newtonsoft.Json;
using System;

public class FarmEntity
{
    public FarmEntityConfig Config { get; private set; }
    public GameConfig GameConfig { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime HarvestedAt { get; private set; }
    public bool IsHarvested { get; private set; } = false;

    public FarmEntity() { }
    public FarmEntity(FarmEntityConfig config, GameConfig gameConfig,
        DateTime CreatedAt, DateTime HarvestedAt, bool IsHarvested) {
        Config = config;
        GameConfig = gameConfig;
        this.CreatedAt = CreatedAt;
        this.HarvestedAt = HarvestedAt;
        this.IsHarvested = IsHarvested;
    }
    public FarmEntity(FarmEntityConfig config, GameConfig gameConfig)
    {
        Config = config;
        GameConfig = gameConfig;
        CreatedAt = DateTime.Now.AddSeconds(gameConfig.WorkerSpeedSeconds);
    }

    public bool IsDead(DateTime now)
    {
        if (IsHarvested) return false;
        float elapsed = (float)(now - CreatedAt).TotalSeconds;
        return elapsed >= Config.HarvestIntervalSeconds * Config.MaxYield + Config.LifetimeSeconds;
    }


    public bool CanHarvest() => CanHarvest(DateTime.Now);
    public bool CanHarvest(DateTime now)
    {
        if (IsHarvested || IsDead(now)) return false;
        float elapsed = (float)(now - CreatedAt).TotalSeconds;
        return elapsed >= Config.HarvestIntervalSeconds * Config.MaxYield;
    }

    public float TotalProducingTime() {
        return Config.HarvestIntervalSeconds * Config.MaxYield;
    }

    public float HarvestingTimeRemains(DateTime now)
    {
        if (IsHarvested || IsDead(now)) return 0;
        float elapsed = (float)(now - CreatedAt).TotalSeconds;
        return Config.HarvestIntervalSeconds * Config.MaxYield + Config.LifetimeSeconds - elapsed;
    }

    public DateTime StartingHarvestTime()
    {
        return CreatedAt.AddSeconds(Config.HarvestIntervalSeconds * Config.MaxYield);
    }

    //public abstract int Harvest(DateTime now);
    public int Harvest(DateTime now)
    {
        IsHarvested = true;
        HarvestedAt = now;
        return Config.MaxYield;
    }
}
