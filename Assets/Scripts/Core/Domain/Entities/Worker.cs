using System;

public class Worker
{
    public float WorkSpeedSeconds { get; private set; }

    public Worker(float workSpeed)
    {
        WorkSpeedSeconds = workSpeed;
    }

    public void PerformTask(FarmEntity entity, DateTime now)
    {
        if (entity.CanHarvest(now))
        {
            entity.Harvest(now);
        }
    }
}
