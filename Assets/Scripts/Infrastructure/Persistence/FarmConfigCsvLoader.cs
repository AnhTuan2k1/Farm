
using System.Collections.Generic;
using System.IO;

public static class FarmConfigCsvLoader
{
    public static List<FarmEntityConfig> LoadFromCsv(string filePath)
    {
        var configs = new List<FarmEntityConfig>();
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++) // bỏ dòng tiêu đề
        {
            var parts = lines[i].Split(',');
            configs.Add(new FarmEntityConfig
            {
                Type = parts[0],
                Name = parts[1],
                HarvestIntervalSeconds = int.Parse(parts[2]),
                MaxYield = int.Parse(parts[3]),
                ProductValue = int.Parse(parts[4]),
                SeedPrice = int.Parse(parts[5]),
                LifetimeSeconds = int.Parse(parts[6])
            });
        }

        return configs;
    }
}
