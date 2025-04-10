using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonFarmSaveService : IFarmRepository
{
    private readonly string savePath;
    private readonly FarmSaveService converter = new();

    public JsonFarmSaveService()
    {
        savePath = Path.Combine(Application.persistentDataPath, "farm_save.json");
    }

    public void Save(Farm farm)
    {
        var dto = converter.ConvertToDTO(farm);
        var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        File.WriteAllText(savePath, json);

    }

    public Farm Load()
    {
        if (!File.Exists(savePath)) return null;
        var json = File.ReadAllText(savePath);
        var dto = JsonConvert.DeserializeObject<FarmDTO>(json);
        return converter.ConvertToFarm(dto);
    }

    public void SaveDB(Farm farm)
    {
        var dto = converter.ConvertToDTO(farm);
        var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        File.WriteAllText(savePath, json);
    }
}
