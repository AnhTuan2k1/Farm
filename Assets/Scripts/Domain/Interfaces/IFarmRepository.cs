
public interface IFarmRepository
{
    Farm Load();
    void Save(Farm data);
    void SaveDB(Farm currentFarm);
}
