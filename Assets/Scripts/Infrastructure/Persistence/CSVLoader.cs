using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVLoader : ICSVLoader
{
    public Dictionary<string, FarmEntityConfig> LoadEntityConfigs(string csvPath)
    {
        var configs = new Dictionary<string, FarmEntityConfig>();
        string path = Path.Combine(Application.streamingAssetsPath, csvPath);
        var lines = File.ReadAllLines(path).Skip(1); // Skip header

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length < 7) continue;

            var config = new FarmEntityConfig
            {
                Name = parts[0],
                Type = parts[1],
                HarvestIntervalSeconds = float.Parse(parts[2]),
                MaxYield = int.Parse(parts[3]),
                ProductValue = int.Parse(parts[4]),
                LifetimeSeconds = float.Parse(parts[5]),
                SeedPrice = int.Parse(parts[6])
            };

            configs[config.Name] = config;
        }

        return configs;
    }

    public GameConfig LoadGameConfig(string csvPath)
    {
        string path = Path.Combine(Application.streamingAssetsPath, csvPath);
        var dict = File.ReadAllLines(path)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .ToDictionary(parts => parts[0], parts => parts[1]);

        return new GameConfig
        {
            LandExpansionCost = int.Parse(dict["LandExpansionCost"]),
            WorkerHireCost = int.Parse(dict["WorkerHireCost"]),
            WorkerSpeedSeconds = float.Parse(dict["WorkerSpeedSeconds"]),
            UpgradeCost = int.Parse(dict["UpgradeCost"]),
            UpgradeMultiplier = float.Parse(dict["UpgradeMultiplier"]),
            StartGold = int.Parse(dict["StartGold"])
        };
    }
}
