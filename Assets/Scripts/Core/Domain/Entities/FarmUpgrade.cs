
public class FarmUpgrade
{
    public int Level { get; private set; } = 1;
    public float UpgradeMultiplier { get; private set; }

    public FarmUpgrade(float upgradeMultiplier)
    {
        UpgradeMultiplier = upgradeMultiplier;
    }

    public float ProductivityMultiplier => 1f + (Level - 1) * UpgradeMultiplier;

    public void Upgrade()
    {
        Level++;
    }
}
