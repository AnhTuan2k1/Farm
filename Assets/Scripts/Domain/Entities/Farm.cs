using System;
using System.Collections.Generic;

public class Farm
{
    public List<LandPlot> LandPlots { get; private set; } = new List<LandPlot>();
    public List<Worker> Workers { get; private set; } = new List<Worker>();
    public FarmUpgrade Upgrade { get; private set; }
    public int Gold { get; private set; }
    public Inventory Inventory { get; private set; }

    private GameConfig config;
    public Farm() { }
    public Farm(List<LandPlot> LandPlots, List<Worker> Workers,
        FarmUpgrade Upgrade, int Gold, Inventory Inventory, GameConfig config) {
        this.LandPlots = LandPlots;
        this.Workers = Workers;
        this.Upgrade = Upgrade;
        this.Gold = Gold;
        this.Inventory = Inventory;
        this.config = config;
    }
    public Farm(GameConfig gameConfig, List<LandPlot> landPlots, Inventory inventory, int workers)
    {
        config = gameConfig;
        LandPlots = landPlots;
        Inventory = inventory;
        Gold = gameConfig.StartGold;
        Upgrade = new FarmUpgrade();
        for (int i = 0; i < workers; i++) Workers.Add(new Worker(config.WorkerSpeedSeconds));
    }

    public void ExpandLand(int index)
    {
        if (Gold >= config.LandExpansionCost)
        {
            SpendGold(config.LandExpansionCost);
            LandPlots[index].UnLock();
        }
    }

    public void HireWorker()
    {
        if (Gold >= config.WorkerHireCost)
        {
            SpendGold(config.WorkerHireCost);
            Workers.Add(new Worker(config.WorkerSpeedSeconds));
        }
    }

    public void UpgradeFarm()
    {
        if (Gold >= config.UpgradeCost)
        {
            SpendGold(config.UpgradeCost);
            Upgrade.Upgrade();
        }
    }

    public void SellAllProduct()
    {
        foreach (var product in Inventory.Products)
        {
            int amount = Inventory.GetProductCount(product.Key);
            if (amount > 0)
            {
                FarmEntityConfig config = GameFarmConfigs.Instance.GetFarmEntityConfig(product.Key);
                if (config != null)
                {
                    Inventory.RemoveProduct(product.Key, amount);
                    AddGold(amount * config.ProductValue);
                }
            }
        }
    }
    public void SellAllProduct(string productName)
    {
        int amount = Inventory.GetProductCount(productName);
        if (amount > 0)
        {
            FarmEntityConfig config = GameFarmConfigs.Instance.GetFarmEntityConfig(productName);
            if (config != null)
            {
                Inventory.RemoveProduct(productName, amount);
                AddGold(amount * config.ProductValue);
            }
        }
    }
    public bool SellProduct(string productName, int amount)
    {
        if (Inventory.GetProductCount(productName) >= amount)
        {
            FarmEntityConfig config = GameFarmConfigs.Instance.GetFarmEntityConfig(productName);
            if (config != null)
            {
                Inventory.RemoveProduct(productName, amount);
                AddGold(Inventory.GetProductCount(productName) * config.ProductValue);
                return true;
            } else return false;
        } else return false;
    }

    public void SpendGold(int amount)
    {
        Gold = Math.Max(Gold - amount, 0);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }



    public bool WorkerSowSeed(string seedName, int landIndex)
    {
        return WorkerHarvest(landIndex);
    }

    public bool WorkerHarvest(int landIndex)
    {
        Worker worker = GetAvailableWorker();
        if (worker != null)
        {
            worker.MarkWorked(DateTime.Now);
            return true;
        }
        else return false;
    }

    public Worker GetAvailableWorker()
    {
        foreach (var worker in Workers)
        {
            if (worker.CanWork(DateTime.Now))
            {
                return worker;
            }
        }
        return null;
    }

    public bool WorkerAvailable()
    {
        foreach (var worker in Workers)
        {
            if (worker.CanWork(DateTime.Now))
            {
                return true;
            }
        }
        return false;
    }

    internal int AvailableWorkers()
    {
        int count = 0;
        foreach (var worker in Workers)
        {
            if (worker.CanWork(DateTime.Now))
            {
                count++;
            }
        }
        return count;
    }

    public int IdleLands()
    {
        int count = 0;
        foreach (var land in LandPlots)
        {
            if (land.Occupant == null && land.IsUnlocked)
            {
                count++;
            }
        }
        return count;
    }

    public int UnlockedleLands()
    {
        int count = 0;
        foreach (var land in LandPlots)
        {
            if (land.IsUnlocked)
            {
                count++;
            }
        }
        return count;
        
    }

    public void Harvest(int landIndex)
    {
        LandPlot land = LandPlots[landIndex];
        FarmEntityConfig config = GameFarmConfigs.Instance.GetFarmEntityConfig(land.Occupant.Config.Name);

        Inventory.AddProduct(config.Name, (int)(config.MaxYield*Upgrade.ProductivityMultiplier));
        land.AssignEntity(null);
    }

    internal bool PrepareForHarvest(int landIndex)
    {
        if (WorkerHarvest(landIndex))
        {
            LandPlot land = LandPlots[landIndex];
            if (land.Occupant != null)
            {
                FarmEntityConfig config = GameFarmConfigs.Instance.GetFarmEntityConfig(land.Occupant.Config.Name);
                if (config != null)
                {
                    land.Occupant.Harvest(DateTime.Now);
                    return true;
                } return false;
            } return false;
        } return false;
    }
}
