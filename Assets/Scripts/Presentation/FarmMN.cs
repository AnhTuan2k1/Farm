using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using UnityEngine;

public class FarmMN : MonoBehaviour
{
    public LandPlotController[] landPlotControllers;
    [SerializeField] private float updateTimer;
    public IFarmRepository farmRepository;

    private Farm currentFarm;
    //public static FarmMN Instance { get; private set; }
    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    Instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}
    //public Farm GetFarm() => currentFarm;

    private void Start()
    {
        var serializer = new FarmSerializer(GameFarmConfigs.Instance.GameConfig, GameFarmConfigs.Instance.FarmEntityConfig.Values.ToList());
        var repository = new FarmRepository(serializer);

        farmRepository = repository; // Use the appropriate repository implementation
        var _loadGameUseCase = new LoadGameUseCase(farmRepository);

        currentFarm = _loadGameUseCase.LoadFarm();
        farmRepository.Save(currentFarm);
        var cropUseCase = new SowSeedUseCase(farmRepository, new EntityFactoryService());
        var expandUseCase = new ExpandLandUseCase(farmRepository, new EntityFactoryService());

        for (int i = 0; i < landPlotControllers.Length; i++)
        {
            landPlotControllers[i].Init(this, cropUseCase, expandUseCase, currentFarm.LandPlots[i], i);
        }

        StartCoroutine(AutoSaveRoutine());
        UIManager.Instance.UpdateAll(currentFarm);
    }

    ///save ////////////////////////////////////////////////////////////////////
    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private IEnumerator AutoSaveRoutine()
    {
        var wait = new WaitForSeconds(300); // 5 phút
        while (true)
        {
            yield return wait;
            Save();
        }
    }

    private void Save()
    {
        if (currentFarm != null)
        {
            farmRepository.SaveDB(currentFarm);
        }
    }
    //////save////////////////////////////////////////////////////////////////


    /// update ui////////////////////////////////////////////////////////////////
    private void Update()
    {
        if (updateTimer < 0.5f)
        {
            updateTimer += Time.deltaTime;
        }
        else
        {
            updateTimer -= 0.5f;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateAll(farmRepository.Load());
        for (int i = 0; i < landPlotControllers.Length; i++)
        {
            landPlotControllers[i].UpdateUI();
        }
    }
    /// update ui////////////////////////////////////////////////////////////////
}
