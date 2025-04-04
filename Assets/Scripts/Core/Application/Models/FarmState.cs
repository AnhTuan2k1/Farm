using System.Collections.Generic;

public class FarmState
{
    public List<LandPlot> LandPlots { get; set; } = new();
    public List<FarmEntity> Inventory { get; set; } = new();
    public int Workers { get; set; }
    public int EquipmentLevel { get; set; }
    public int Gold { get; set; }
}
