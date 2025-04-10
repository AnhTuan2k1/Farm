
using System.Collections.Generic;

public class GameFarmConfigs
{
    private readonly ICSVLoader _CSVLoader;
    private Dictionary<string, FarmEntityConfig> farmEntityConfig;
    private GameConfig gameConfig;

    private const string entityConfigsPath = "EntityConfigs.csv";
    private const string gameConfigPath = "GameConfig.csv";

    public static GameFarmConfigs Instance { get; } = new GameFarmConfigs(new CSVLoader());

    private GameFarmConfigs(ICSVLoader CSVLoader)
    {
        _CSVLoader = CSVLoader;
    }

    public Dictionary<string, FarmEntityConfig> FarmEntityConfig
    {
        get
        {
            if (farmEntityConfig == null)
            {
                farmEntityConfig = _CSVLoader.LoadEntityConfigs(entityConfigsPath);
            }
            return farmEntityConfig;
        }
    }

    public GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
            {
                gameConfig = _CSVLoader.LoadGameConfig(gameConfigPath);
            }
            return gameConfig;
        }
    }
    public FarmEntityConfig GetFarmEntityConfig(string id)
    {
        if (farmEntityConfig == null)
        {
            farmEntityConfig = _CSVLoader.LoadEntityConfigs(entityConfigsPath);
        }
        return farmEntityConfig.TryGetValue(id, out var config) ? config : null;
    }
}
