
public class SowSeedUseCase
{
    private readonly IEntityFactoryService entityFactory;
    private readonly IFarmRepository farmRepository;

    public SowSeedUseCase(
        IFarmRepository farmRepository,
        IEntityFactoryService entityFactory)
    {
        this.farmRepository = farmRepository;
        this.entityFactory = entityFactory;
    }

    public string Execute(string entityName, string type, int landIndex)
    {
        if (farmRepository.Load().Inventory.GetSeedCount(entityName)<=0) return "not enough seed";
        if (!farmRepository.Load().WorkerAvailable()) return "not enough worker";

        var crop = entityFactory.CreateFarmEntity(entityName);
        var farm = farmRepository.Load();

        farm.LandPlots[landIndex].AssignEntity(crop);
        farm.Inventory.ConsumeSeed(entityName);
        farm.WorkerSowSeed(entityName, landIndex);

        return null;
    }
}