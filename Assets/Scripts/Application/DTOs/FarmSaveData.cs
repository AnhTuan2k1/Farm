using System.Collections.Generic;
using System;

public class FarmSaveData
{
    public List<LandPlotSaveData> LandPlots { get; set; }
    public List<WorkerSaveData> Workers { get; set; }
    public FarmUpgradeSaveData Upgrade { get; set; }
    public int Gold { get; set; }
    public InventorySaveData Inventory { get; set; }
}

public class LandPlotSaveData
{
    public bool IsUnlocked { get; set; }
    public int Index { get; set; }
    public FarmEntitySaveData Occupant { get; set; }
}

public class FarmEntitySaveData
{
    public string ConfigName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime HarvestedAt { get; set; }
    public bool IsHarvested { get; set; }
}

public class WorkerSaveData
{
    public float WorkDurationSeconds { get; set; }
    public DateTime LastWorkedAt { get; set; }
}

public class FarmUpgradeSaveData
{
    public int Level { get; set; }
    public float UpgradeMultiplier { get; set; }
}

public class InventorySaveData
{
    public Dictionary<string, int> Seeds { get; set; }
    public Dictionary<string, int> Products { get; set; }
    public Dictionary<string, int> TotalHarvested { get; set; }
}