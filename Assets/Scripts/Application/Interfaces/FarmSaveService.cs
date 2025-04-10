using System;
using System.Collections.Generic;
using System.Linq;

public class FarmSaveService
{
    public FarmDTO ConvertToDTO(Farm farm)
    {
        return new FarmDTO
        {
            Gold = farm.Gold,
            Upgrade = new FarmUpgradeDTO { Level = farm.Upgrade.Level },
            LandPlots = farm.LandPlots.Select(lp => new LandPlotDTO
            {
                Id = lp.Index,
                IsUnlocked = lp.IsUnlocked,
                Occupant = lp.Occupant != null ? new FarmEntityDTO
                {
                    CreatedAt = lp.Occupant.CreatedAt.ToString("o"),
                    HarvestedAt = lp.Occupant.HarvestedAt.ToString("o"),
                    IsHarvested = lp.Occupant.IsHarvested
                } : null
            }).ToList(),
            Workers = farm.Workers.Select(w => new WorkerDTO
            {
                LastWorkedAt = w.LastWorkedAt.ToString("o")
            }).ToList(),
            Inventory = new InventoryDTO
            {
                seeds = ReflectionUtils.GetPrivateField<Dictionary<string, int>>(farm.Inventory, "seeds"),
                products = ReflectionUtils.GetPrivateField<Dictionary<string, int>>(farm.Inventory, "products"),
                totalHarvested = ReflectionUtils.GetPrivateField<Dictionary<string, int>>(farm.Inventory, "totalHarvested")
            }
        };
    }

    public Farm ConvertToFarm(FarmDTO dto)
    {
        var farm = new Farm();
        ReflectionUtils.SetPrivateField(farm, "Gold", dto.Gold);
        ReflectionUtils.SetPrivateField(farm, "Upgrade", new FarmUpgradeWrapper(dto.Upgrade.Level));
        ReflectionUtils.SetPrivateField(farm, "Workers", dto.Workers.Select(w =>
            new WorkerWrapper(DateTime.Parse(w.LastWorkedAt))).ToList());
        ReflectionUtils.SetPrivateField(farm, "LandPlots", dto.LandPlots.Select(lp =>
            new LandPlotWrapper(lp.Id, lp.IsUnlocked, lp.Occupant)).ToList());
        ReflectionUtils.SetPrivateField(farm, "Inventory", new InventoryWrapper(dto.Inventory));

        return farm;
    }
}
