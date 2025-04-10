
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

public static class FarmEntityName
{
    public const string Blueberry = "Blueberry";
    public const string Tomato = "Tomato";
    public const string Cow = "Cow";
    public static string Strawberry = "Strawberry";
}