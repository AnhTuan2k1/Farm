
public class FarmUpgrade
{
    public int Level { get; private set; } = 1;
    public float UpgradeMultiplier { get; private set; }
    public FarmUpgrade(int Level)
    {
        this.Level = Level;
        UpgradeMultiplier = GameFarmConfigs.Instance.GameConfig.UpgradeMultiplier;
    }
    public FarmUpgrade()
    {
        UpgradeMultiplier = GameFarmConfigs.Instance.GameConfig.UpgradeMultiplier;
    }

    public float ProductivityMultiplier => 1f + (Level - 1) * UpgradeMultiplier;

    public void Upgrade()
    {
        Level++;
    }
}
