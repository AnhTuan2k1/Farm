using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class FarmSerializer
{
    private readonly GameConfig gameConfig;
    private readonly Dictionary<string, FarmEntityConfig> entityConfigs;

    public FarmSerializer(GameConfig config, List<FarmEntityConfig> configs)
    {
        gameConfig = config;
        entityConfigs = configs.ToDictionary(c => c.Name, c => c);

    }

    public string Serialize(Farm farm)
    {
        var data = new FarmSaveData
        {
            Gold = farm.Gold,
            Workers = farm.Workers.Select(w => new WorkerSaveData
            {
                WorkDurationSeconds = w.WorkDurationSeconds,
                LastWorkedAt = w.LastWorkedAt
            }).ToList(),
            LandPlots = farm.LandPlots.Select(p => new LandPlotSaveData
            {
                Index = p.Index,
                IsUnlocked = p.IsUnlocked,
                Occupant = p.Occupant == null ? null : new FarmEntitySaveData
                {
                    ConfigName = p.Occupant.Config.Name,
                    CreatedAt = p.Occupant.CreatedAt,
                    HarvestedAt = p.Occupant.HarvestedAt,
                    IsHarvested = p.Occupant.IsHarvested
                }
            }).ToList(),
            Upgrade = new FarmUpgradeSaveData
            {
                Level = farm.Upgrade.Level,
                UpgradeMultiplier = farm.Upgrade.UpgradeMultiplier
            },
            Inventory = new InventorySaveData
            {
                Seeds = GetPrivateDict(farm.Inventory, "seeds"),
                Products = GetPrivateDict(farm.Inventory, "products"),
                TotalHarvested = GetPrivateDict(farm.Inventory, "totalHarvested")
            }
        };

        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    public Farm Deserialize(string json)
    {
        var data = JsonConvert.DeserializeObject<FarmSaveData>(json);
        List<LandPlot> landPlots = data.LandPlots.Select(p =>
        {        
            var index = p.Index;
            var occupant = p.Occupant != null ? new FarmEntity(entityConfigs.ContainsKey(p.Occupant.ConfigName)
            ? entityConfigs[p.Occupant.ConfigName] : null, gameConfig, p.Occupant.CreatedAt, p.Occupant.HarvestedAt, p.Occupant.IsHarvested) : null;
            var isUnlocked = p.IsUnlocked;

            return new LandPlot(index, occupant, isUnlocked);
        }).ToList();

        var workers = data.Workers.Select(w =>
        {
            return new Worker(gameConfig.WorkerSpeedSeconds, w.LastWorkedAt);
        }).ToList();

        var upgrade = new FarmUpgrade(data.Upgrade.Level);

        var gold = data.Gold;

        var inventory = new Inventory(data.Inventory.Seeds, data.Inventory.Products, data.Inventory.TotalHarvested);

        var farm = new Farm(landPlots, workers, upgrade, gold, inventory, gameConfig);
        return farm;
    }

    private Dictionary<string, int> GetPrivateDict(Inventory inventory, string field)
    {
        var fi = typeof(Inventory).GetField(field, BindingFlags.NonPublic | BindingFlags.Instance);
        return fi.GetValue(inventory) as Dictionary<string, int>;
    }

    private void SetPrivateField(object target, string field, object value)
    {
        var fi = target.GetType().GetField(field, BindingFlags.NonPublic | BindingFlags.Instance);
        fi.SetValue(target, value);
    }
}
