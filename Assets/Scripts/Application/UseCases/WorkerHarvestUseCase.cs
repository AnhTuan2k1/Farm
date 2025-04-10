using System;

public class WorkerHarvestUseCase
{
    private readonly IFarmRepository _farmRepository;

    public WorkerHarvestUseCase(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public bool Execute(int landIndex)
    {
        var farm = _farmRepository.Load();
        return farm.WorkerHarvest(landIndex);
    }
}
