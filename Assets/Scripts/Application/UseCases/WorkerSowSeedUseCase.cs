
public class WorkerSowSeedUseCase
{
    private readonly IFarmRepository _farmRepository;

    public WorkerSowSeedUseCase(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public bool Execute(string seedName, int landIndex)
    {
        var farm = _farmRepository.Load();
        return farm.WorkerSowSeed(seedName, landIndex);
    }
}
