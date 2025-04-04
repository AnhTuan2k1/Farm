using System.Collections.Generic;

public class Farm
{
    public List<LandPlot> LandPlots { get; private set; } = new List<LandPlot>();
    public List<Worker> Workers { get; private set; } = new List<Worker>();
    public FarmUpgrade Upgrade { get; private set; }
    public int Gold { get; private set; }
    private GameConfig config;

    public Farm(int initialGold, int initialLandPlots, int initialWorkers, GameConfig gameConfig)
    {
        config = gameConfig;
        Gold = initialGold;
        Upgrade = new FarmUpgrade(config.UpgradeMultiplier);

        for (int i = 0; i < initialLandPlots; i++) LandPlots.Add(new LandPlot());
        for (int i = 0; i < initialWorkers; i++) Workers.Add(new Worker(config.WorkerSpeedSeconds));
    }

    public void ExpandLand()
    {
        if (Gold >= config.LandExpansionCost)
        {
            Gold -= config.LandExpansionCost;
            LandPlots.Add(new LandPlot());
        }
    }

    public void HireWorker()
    {
        if (Gold >= config.WorkerHireCost)
        {
            Gold -= config.WorkerHireCost;
            Workers.Add(new Worker(config.WorkerSpeedSeconds));
        }
    }

    public void UpgradeFarm()
    {
        if (Gold >= config.UpgradeCost)
        {
            Gold -= config.UpgradeCost;
            Upgrade.Upgrade();
        }
    }
}
