using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gold_text;
    [SerializeField] private TMP_Text tool_level_text;
    [SerializeField] private TMP_Text worker_idle_text;
    [SerializeField] private TMP_Text seed_text;
    [SerializeField] private TMP_Text land_text;
    [SerializeField] private TMP_Text tomato_harvested_text;
    [SerializeField] private TMP_Text blueberry_harvested_text;
    [SerializeField] private TMP_Text cow_harvested_text;
    [SerializeField] private TMP_Text tomato_seeds_text;
    [SerializeField] private TMP_Text blueberry_seeds_text;
    [SerializeField] private TMP_Text cow_seeds_text;
    [SerializeField] private TMP_Text strawberry_seeds_text;

    [SerializeField] private Button upgrade_tool_btn;
    [SerializeField] private Button hire_worker_btn;
    [SerializeField] private Button store_btn;
    [SerializeField] private Button inventory_btn;

    [SerializeField] private FarmMN farmMN;

    private float doubleTapThreshold = 0.3f; // Giây
    private float lastTapTime = -1f;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<UIManager>();
            return _instance;
        }
    }

    private void Start()
    {
        upgrade_tool_btn.onClick.AddListener(OnUpgradeToolClicked);
        hire_worker_btn.onClick.AddListener(OnHireWorkerClicked);
        store_btn.onClick.AddListener(OnStoreClicked);
        inventory_btn.onClick.AddListener(OnInventoryClicked);

        if(farmMN== null)
        {
            farmMN = FindFirstObjectByType<FarmMN>();
        }
    }

    public void UpdateAll(Farm farm)
    {
        UpdateGold(farm.Gold);
        UpdateToolLevel(farm.Upgrade.Level);
        UpdateWorkerIdle(farm.AvailableWorkers(), farm.Workers.Count);
        UpdateSeed(farm.Inventory.GetTotalSeedCount());
        UpdateLand(farm.IdleLands(), farm.UnlockedleLands());
        UpdateTomatoHarvested(farm.Inventory.GetTotalHarvested(FarmEntityName.Tomato));
        UpdateBlueberryHarvested(farm.Inventory.GetTotalHarvested(FarmEntityName.Blueberry));
        UpdateCowHarvested(farm.Inventory.GetTotalHarvested(FarmEntityName.Cow));
        UpdateTomatoSeeds(farm.Inventory.GetSeedCount(FarmEntityName.Tomato));
        UpdateBlueberrySeeds(farm.Inventory.GetSeedCount(FarmEntityName.Blueberry));
        UpdateCowSeeds(farm.Inventory.GetSeedCount(FarmEntityName.Cow));
        UpdateStrawberrySeeds(farm.Inventory.GetSeedCount(FarmEntityName.Strawberry));
    }

    private void UpdateTomatoSeeds(int v)
    {
        tomato_seeds_text.text = v.ToString();
    }

    private void UpdateBlueberrySeeds(int v)
    {
        blueberry_seeds_text.text = v.ToString();
    }

    private void UpdateCowSeeds(int v)
    {
        cow_seeds_text.text = v.ToString();
    }

    private void UpdateStrawberrySeeds(int v)
    {
        strawberry_seeds_text.text = v.ToString();
    }

    private void UpdateGold(int gold)
    {
        gold_text.text = gold.ToString();
    }
    private void UpdateToolLevel(int level)
    {
        tool_level_text.text = level.ToString();
    }
    private void UpdateWorkerIdle(int idle, int total)
    {
        worker_idle_text.text = $"{idle}/{total}";
    }
    private void UpdateSeed(int amount)
    {
        seed_text.text = amount.ToString();
    }
    private void UpdateLand(int idle, int total)
    {
        land_text.text = $"{idle}/{total}";
    }
    private void UpdateTomatoHarvested(int amount)
    {
        tomato_harvested_text.text = amount.ToString();
    }
    private void UpdateBlueberryHarvested(int amount)
    {
        blueberry_harvested_text.text = amount.ToString();
    }
    private void UpdateCowHarvested(int amount)
    {
        cow_harvested_text.text = amount.ToString();
    }

    public void UpdateWorker()
    {
        Farm farm = farmMN.farmRepository.Load();
        UpdateWorkerIdle(farm.AvailableWorkers(), farm.Workers.Count);
    }

    private void OnInventoryClicked()
    {
        InventoryCanvas.Instance.Show();
    }

    private void OnStoreClicked()
    {
        ShopCanvas.Instance.Show();
    }

    private void OnHireWorkerClicked()
    {
        float currentTime = Time.time;

        if (currentTime - lastTapTime < doubleTapThreshold)
        {
            // Double tap detected
            lastTapTime = -1f; // reset
            Farm farm = farmMN.farmRepository.Load();
            farm.HireWorker();
            UpdateWorkerIdle(farm.AvailableWorkers(), farm.Workers.Count);
        }
        else
        {
            // First tap
            lastTapTime = currentTime;
        }
    }

    private void OnUpgradeToolClicked()
    {
        float currentTime = Time.time;

        if (currentTime - lastTapTime < doubleTapThreshold)
        {
            // Double tap detected
            lastTapTime = -1f; // reset
            Farm farm = farmMN.farmRepository.Load();
            farm.UpgradeFarm();
            UpdateToolLevel(farm.Upgrade.Level);
        }
        else
        {
            // First tap
            lastTapTime = currentTime;
        }
    }
}
