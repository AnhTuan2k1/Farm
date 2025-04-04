
using System;

public class FarmEntityFactory : IFarmEntityFactory
{
    public FarmEntity CreateEntity(FarmEntityConfig config)
    {
        return config.Type switch
        {
            "Crop" => new Crop(config),

            "Animal" => new Animal(config),

            _ => throw new ArgumentException($"Unknown entity type: {config.Type}")
        };
    }
}
