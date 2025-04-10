using System.IO;
using UnityEngine;

public class FarmRepository : IFarmRepository
{
    private readonly FarmSerializer serializer;
    private readonly string saveFilePath;
    private static Farm farm1;

    public FarmRepository(FarmSerializer serializer)
    {
        this.serializer = serializer;
        this.saveFilePath = Application.persistentDataPath + "/farm_save.json";
    }

    public void Save(Farm farm)
    {
        farm1 = farm;
    }

    public Farm Load()
    {
        if(farm1 != null)
        {
            return farm1;
        }
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("Save file not found. Returning null.");
            return null;
        }

        string json = File.ReadAllText(saveFilePath);
        return serializer.Deserialize(json);
    }

    public void SaveDB(Farm currentFarm)
    {
        string json = serializer.Serialize(currentFarm);
        File.WriteAllText(saveFilePath, json);
    }
}
