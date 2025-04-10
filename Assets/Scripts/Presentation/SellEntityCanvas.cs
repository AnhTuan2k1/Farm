using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellEntityCanvas : MonoBehaviour
{
    [SerializeField] private string entityName;
    [SerializeField] private TMP_Text entityText;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Slider slider;
    [SerializeField] private Button sell;
    [SerializeField] private Button cancel;
    private GameConfig gameConfig;
    private FarmMN farmMN;

    private void Start()
    {
        sell.onClick.AddListener(OnSellButtonClicked);
        cancel.onClick.AddListener(OnCancelButtonClicked);
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        gameConfig = GameFarmConfigs.Instance.GameConfig;
        farmMN = FindFirstObjectByType<FarmMN>();
    }

    public void Show(string name)
    {
        gameObject.SetActive(true);

        entityName = name;
        int products = farmMN.farmRepository.Load().Inventory.GetProductCount(name);
        var entityconfig = GameFarmConfigs.Instance.GetFarmEntityConfig(name);

        entityText.text = name;
        slider.maxValue = products;
        slider.value = 0;
        amountText.text = "0";
    }

    private void OnSliderValueChanged(float arg0)
    {
        slider.value = (int)arg0;
        amountText.text = ((int)arg0).ToString();
    }

    private void OnCancelButtonClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnSellButtonClicked()
    {
        int amount = (int)slider.value;
        var entityconfig = GameFarmConfigs.Instance.GetFarmEntityConfig(entityName);
        int totalGold = amount * entityconfig.ProductValue;

        farmMN.farmRepository.Load().Inventory.RemoveProduct(entityName, amount);
        farmMN.farmRepository.Load().AddGold(totalGold);


        //gameObject.SetActive(false);
        InventoryCanvas.Instance.Show();
    }


}
