using System;

public class LandPlot
{
    public int Index { get; private set; }
    public FarmEntity Occupant { get; private set; }
    public bool IsUnlocked { get; private set; }

    public LandPlot() { }
    public LandPlot(int index, FarmEntity occupant, bool IsUnlocked)
    {
        this.Index = index;
        Occupant = occupant;
        this.IsUnlocked = IsUnlocked;
    }
    public LandPlot(int index)
    {
        this.Index = index;
        IsUnlocked = false;
    }

    public LandPlot(int index, bool isUnlocked) : this(index)
    {
        IsUnlocked = isUnlocked;
    }

    public void AssignEntity(FarmEntity entity)
    {
        Occupant = entity;
    }

    public void UnLock()
    {
        IsUnlocked = true;
    }

    public bool IsPreparing()
    {
        return Occupant != null && DateTime.Now < Occupant.CreatedAt;
    }

    public bool IsProducing()
    {
        return Occupant != null && DateTime.Now >= Occupant.CreatedAt && DateTime.Now <= Occupant.StartingHarvestTime();
    }

    public bool WaittingForHarvesting()
    {
        return Occupant != null && DateTime.Now >= Occupant.CreatedAt && Occupant.CanHarvest(DateTime.Now);
    }

    public bool IsHarvesting()
    {
        return Occupant != null && Occupant.IsHarvested && DateTime.Now >= Occupant.HarvestedAt && DateTime.Now <= Occupant.HarvestedAt.AddSeconds(Occupant.GameConfig.WorkerSpeedSeconds);
    } 
    
    public bool IsHarvestingDone()
    {
        return Occupant != null && Occupant.IsHarvested && DateTime.Now >= Occupant.HarvestedAt.AddSeconds(Occupant.GameConfig.WorkerSpeedSeconds);
    }

    public float Progress()
    {
        if (IsPreparing())
        {
            float current = (float)(Occupant.CreatedAt - DateTime.Now).TotalSeconds;
            float total = GameFarmConfigs.Instance.GameConfig.WorkerSpeedSeconds;

            return 1 - current / total;
        }
        else if (IsProducing())
        {
            float current = (float)(DateTime.Now - Occupant.CreatedAt).TotalSeconds;
            float total = Occupant.TotalProducingTime();

            //Debug.Log($"current: {current}");
            //Debug.Log($"total: {total}");
            //Debug.Log($"Progress: {current / total}");

            return current / total;
        }
        else if (WaittingForHarvesting())
        {
            return 1;
        } 
        else return 0;
    }

    public float ProgressTimeLeft()
    {
        if (IsPreparing()) {
            float current = (float)(Occupant.CreatedAt - DateTime.Now).TotalSeconds;
            float total = GameFarmConfigs.Instance.GameConfig.WorkerSpeedSeconds;

            return current;
        }
        else if (IsProducing())
        {
            float current = (float)(DateTime.Now - Occupant.CreatedAt).TotalSeconds;
            float total = Occupant.TotalProducingTime();

            return total - current;
        }
        else if (WaittingForHarvesting())
        {
            return Occupant.HarvestingTimeRemains(DateTime.Now);
        }
        else return 0;
    }


    public string ProgressTimeStringhhmmss()
    { 
        float seconds = ProgressTimeLeft();
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string result = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)time.TotalHours, time.Minutes, time.Seconds);
        return result;
    }

}
