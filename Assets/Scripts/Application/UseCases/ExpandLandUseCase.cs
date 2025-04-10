
public class ExpandLandUseCase
{
    private readonly IFarmRepository gameDataRepository;
    private readonly IEntityFactoryService _entityFactory;
    private Farm farm;
    private Inventory inventory;

    public ExpandLandUseCase(
        IFarmRepository farmRepository,
        IEntityFactoryService entityFactory)
    {
        this.gameDataRepository = farmRepository;
        var data = farmRepository.Load();
        this.farm = data;
        this.inventory = data.Inventory;
    }

    public string Execute(int landIndex)
    {
        if (farm.Gold <= GameFarmConfigs.Instance.GameConfig.LandExpansionCost) return "not enough Gold";

        farm.ExpandLand(landIndex);
        return null;
    }
}
