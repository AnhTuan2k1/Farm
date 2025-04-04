
public class FarmEntityConfig
{
    public string Name { get; set; } = string.Empty;
    public float HarvestIntervalSeconds { get; set; }
    public int MaxYield { get; set; }
    public int ProductValue { get; set; }
    public float LifetimeSeconds { get; set; }
    public int SeedPrice { get; set; }
    public string Type { get; set; } = string.Empty;
}
