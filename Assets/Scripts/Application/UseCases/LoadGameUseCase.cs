using System.Collections.Generic;

public class LoadGameUseCase
{
    private readonly IFarmRepository farmRepository;
    private const int maxSize = 81;

    public LoadGameUseCase(IFarmRepository farmRepository)
    {
        this.farmRepository = farmRepository;
    }

    public Farm LoadFarm()
    {
        return farmRepository.Load() ?? InitializeNewGame();
    }

    private Farm InitializeNewGame()
    {
        var landPlots = new List<LandPlot>();
        for (int i = 0; i < maxSize; i++)
        {
            landPlots.Add(new LandPlot(i, isUnlocked: i >= maxSize/2-1 && i <= maxSize/2+1));
        }

        var inventory = new Inventory();
        inventory.AddSeeds(FarmEntityName.Tomato, 10);
        inventory.AddSeeds(FarmEntityName.Blueberry, 10);
        inventory.AddSeeds(FarmEntityName.Cow, 2);

        var farm = new Farm(GameFarmConfigs.Instance.GameConfig, landPlots, inventory, 2);
        return farm;
    }
}

