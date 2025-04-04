using System.Collections.Generic;
using System.Linq;

public class InitializeFarmService
{
    private readonly IFarmEntityFactory _factory;
    private readonly List<FarmEntityConfig> _configs;

    public InitializeFarmService(IFarmEntityFactory factory, List<FarmEntityConfig> configs)
    {
        _factory = factory;
        _configs = configs;
    }

    public FarmState CreateInitialFarm()
    {
        var farm = new FarmState();

        // 1. T?o 3 m?nh ??t tr?ng
        for (int i = 0; i < 3; i++)
            farm.LandPlots.Add(new LandPlot());

        // 2. L?y config
        var tomatoConfig = _configs.First(c => c.Name == "Tomato");
        var blueberryConfig = _configs.First(c => c.Name == "Blueberry");
        var cowConfig = _configs.First(c => c.Name == "DairyCow");

        // 3. T?o 10 cà chua, 10 vi?t qu?t, 2 bò s?a
        for (int i = 0; i < 10; i++)
            farm.Inventory.Add(_factory.CreateEntity(tomatoConfig));

        for (int i = 0; i < 10; i++)
            farm.Inventory.Add(_factory.CreateEntity(blueberryConfig));

        for (int i = 0; i < 2; i++)
            farm.Inventory.Add(_factory.CreateEntity(cowConfig));

        // 4. Công nhân và thi?t b?
        farm.Workers = 1;
        farm.EquipmentLevel = 1;
        farm.Gold = 0; // ho?c 100 n?u b?n mu?n cho v?n

        return farm;
    }
}
