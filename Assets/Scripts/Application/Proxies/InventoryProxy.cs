using System.Collections.Generic;
using System.Reflection;

public class InventoryProxy : Inventory
{
    //public InventoryProxy(InventorySaveData data)
    //{
    //    SetDict("seeds", data.seedKeys, data.seedValues);
    //    SetDict("products", data.productKeys, data.productValues);
    //    SetDict("totalHarvested", data.totalHarvestedKeys, data.totalHarvestedValues);
    //}

    //void SetDict(string fieldName, List<string> keys, List<int> values)
    //{
    //    var dict = new Dictionary<string, int>();
    //    for (int i = 0; i < keys.Count; i++) dict[keys[i]] = values[i];
    //    typeof(Inventory)
    //        .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
    //        ?.SetValue(this, dict);
    //}
}
