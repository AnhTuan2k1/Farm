
public class EntityFactoryService : IEntityFactoryService
{
    public FarmEntity CreateFarmEntity(string id)
    {
        var config = GameFarmConfigs.Instance.GetFarmEntityConfig(id);
        if (config != null)
        {
            return new FarmEntity(config, GameFarmConfigs.Instance.GameConfig);
        }
        return null;
    }

    //public Crop CreateCrop(string id)
    //{
    //    var config = GameFarmConfigs.Instance.GetFarmEntityConfig(id);
    //    if (config != null)
    //    {
    //        return new Crop(config);
    //    }
    //    return null;
    //}

    //public Animal CreateAnimal(string id)
    //{
    //    var config = GameFarmConfigs.Instance.GetFarmEntityConfig(id);
    //    if (config != null)
    //    {
    //        return new Animal(config);
    //    }
    //    return null;
    //}
}
