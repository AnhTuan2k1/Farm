using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] private Button tomatoBuybtn;
    [SerializeField] private Button blueberryBuybtn;
    [SerializeField] private Button cowBuybtn;
    [SerializeField] private Button strawberryBuybtn;

    [SerializeField] private Slider tomatoSlider;
    [SerializeField] private Slider blueberrySlider;
    [SerializeField] private Slider cowSlider;
    [SerializeField] private Slider strawberrySlider;

    [SerializeField] private TMP_Text tomatoNumText;
    [SerializeField] private TMP_Text blueberryNumText;
    [SerializeField] private TMP_Text cowNumText;
    [SerializeField] private TMP_Text strawberryNumText;

    [SerializeField] private Button closeButton;

    private static ShopCanvas _instance;
    public static ShopCanvas Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<ShopCanvas>();
            return _instance;
        }
    }

    private void Start()
    {
        if (Instance == null) _instance = this;
        tomatoBuybtn.onClick.AddListener(() => OnBuyButtonClicked(FarmEntityName.Tomato));
        blueberryBuybtn.onClick.AddListener(() => OnBuyButtonClicked(FarmEntityName.Blueberry));
        cowBuybtn.onClick.AddListener(() => OnBuyButtonClicked(FarmEntityName.Cow));
        strawberryBuybtn.onClick.AddListener(() => OnBuyButtonClicked(FarmEntityName.Strawberry));

        closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        tomatoSlider.onValueChanged.AddListener((value) => tomatoNumText.text = ((int)value).ToString());
        blueberrySlider.onValueChanged.AddListener((value) => blueberryNumText.text = ((int)value).ToString());
        cowSlider.onValueChanged.AddListener((value) => cowNumText.text = ((int)value).ToString());
        strawberrySlider.onValueChanged.AddListener((value) => strawberryNumText.text = ((int)value*10).ToString());

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        tomatoSlider.value = 0;
        blueberrySlider.value = 0;
        cowSlider.value = 0;
        strawberrySlider.value = 0;

        var farm = FindFirstObjectByType<FarmMN>().farmRepository.Load();

        tomatoSlider.maxValue = farm.Gold/ GameFarmConfigs.Instance.GetFarmEntityConfig(FarmEntityName.Tomato).ProductValue;
        blueberrySlider.maxValue = farm.Gold / GameFarmConfigs.Instance.GetFarmEntityConfig(FarmEntityName.Blueberry).ProductValue;
        cowSlider.maxValue = farm.Gold / GameFarmConfigs.Instance.GetFarmEntityConfig(FarmEntityName.Cow).ProductValue;
        strawberrySlider.maxValue = (farm.Gold / GameFarmConfigs.Instance.GetFarmEntityConfig(FarmEntityName.Strawberry).ProductValue)/10;

        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnBuyButtonClicked(string name)
    {
        int amount = GetValue(name);
        var entityconfig = GameFarmConfigs.Instance.GetFarmEntityConfig(name);
        int totalGold = amount * entityconfig.ProductValue;

        var farm = FindFirstObjectByType<FarmMN>().farmRepository.Load();
        if (farm.Gold >= totalGold)
        {
            farm.Inventory.AddSeeds(name, amount);
            farm.AddGold(-totalGold);
            UIManager.Instance.UpdateAll(farm);
            Show();
        }
        else
        {
            Debug.Log("Not enough gold to buy seeds.");
        }
    }

    private int GetValue(string name)
    {
        if (name == FarmEntityName.Tomato)
        {
            return (int)tomatoSlider.value;
        }
        else if (name == FarmEntityName.Blueberry)
        {
            return (int)blueberrySlider.value;
        }
        else if (name == FarmEntityName.Cow)
        {
            return (int)cowSlider.value;
        }
        else if (name == FarmEntityName.Strawberry)
        {
            return (int)strawberrySlider.value*10;
        }
        return 0;
    }
}
