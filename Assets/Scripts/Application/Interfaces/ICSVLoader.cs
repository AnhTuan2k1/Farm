using System.Collections.Generic;

public interface ICSVLoader
{
    Dictionary<string, FarmEntityConfig> LoadEntityConfigs(string csvPath);
    GameConfig LoadGameConfig(string csvPath);
}
