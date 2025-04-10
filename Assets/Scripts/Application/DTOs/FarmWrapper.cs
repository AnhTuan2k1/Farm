using System;

public class WorkerWrapper : Worker
{
    public WorkerWrapper(DateTime lastWorkedAt) =>
        ReflectionUtils.SetPrivateBackingField(this, nameof(LastWorkedAt), lastWorkedAt);
}

public class FarmUpgradeWrapper : FarmUpgrade
{
    public FarmUpgradeWrapper(int level) =>
        ReflectionUtils.SetPrivateBackingField(this, nameof(Level), level);
}

public class FarmEntityWrapper : FarmEntity
{
    public FarmEntityWrapper(DateTime createdAt, DateTime harvestedAt, bool isHarvested)
    {
        ReflectionUtils.SetPrivateBackingField(this, nameof(CreatedAt), createdAt);
        ReflectionUtils.SetPrivateBackingField(this, nameof(HarvestedAt), harvestedAt);
        ReflectionUtils.SetPrivateBackingField(this, nameof(IsHarvested), isHarvested);
    }
}

public class LandPlotWrapper : LandPlot
{
    public LandPlotWrapper(int id, bool isUnlocked, FarmEntityDTO occupantDTO)
    {
        ReflectionUtils.SetPrivateBackingField(this, nameof(Index), id);
        ReflectionUtils.SetPrivateBackingField(this, nameof(IsUnlocked), isUnlocked);

        if (occupantDTO != null)
        {
            var occupant = new FarmEntityWrapper(
                DateTime.Parse(occupantDTO.CreatedAt),
                DateTime.Parse(occupantDTO.HarvestedAt),
                occupantDTO.IsHarvested
            );
            ReflectionUtils.SetPrivateBackingField(this, nameof(Occupant), occupant);
        }
    }
}

public class InventoryWrapper : Inventory
{
    public InventoryWrapper(InventoryDTO dto)
    {
        ReflectionUtils.SetPrivateField(this, "seeds", dto.seeds);
        ReflectionUtils.SetPrivateField(this, "products", dto.products);
        ReflectionUtils.SetPrivateField(this, "totalHarvested", dto.totalHarvested);
    }
}
