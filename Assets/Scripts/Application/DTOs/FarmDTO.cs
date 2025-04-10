using System.Collections.Generic;
using System;

[Serializable]
public class FarmDTO
{
    public List<LandPlotDTO> LandPlots;
    public List<WorkerDTO> Workers;
    public FarmUpgradeDTO Upgrade;
    public int Gold;
    public InventoryDTO Inventory;
}

[Serializable]
public class LandPlotDTO
{
    public int Id;
    public bool IsUnlocked;
    public FarmEntityDTO Occupant;
}

[Serializable]
public class FarmEntityDTO
{
    public string CreatedAt;
    public string HarvestedAt;
    public bool IsHarvested;
}

[Serializable]
public class WorkerDTO
{
    public string LastWorkedAt;
}

[Serializable]
public class FarmUpgradeDTO
{
    public int Level;
}

[Serializable]
public class InventoryDTO
{
    public Dictionary<string, int> seeds;
    public Dictionary<string, int> products;
    public Dictionary<string, int> totalHarvested;
}
