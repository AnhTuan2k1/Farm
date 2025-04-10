using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    [SerializeField] private Button sellTomatoButton;
    [SerializeField] private Button sellBlueberryButton;
    [SerializeField] private Button sellCowButton;
    [SerializeField] private Button sellStrawBerryButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private TMP_Text sellCowText;
    [SerializeField] private TMP_Text sellTomatoText;
    [SerializeField] private TMP_Text sellBlueberryText;
    [SerializeField] private TMP_Text sellStrawBerryText;

    [SerializeField] private SellEntityCanvas sellEntityCanvas;

    private static InventoryCanvas _instance;
    public static InventoryCanvas Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<InventoryCanvas>();
            return _instance;
        }
    }

    private void Start()
    {
        sellTomatoButton.onClick.AddListener(() => OnSellButtonClicked(FarmEntityName.Tomato));
        sellBlueberryButton.onClick.AddListener(() => OnSellButtonClicked(FarmEntityName.Blueberry));
        sellCowButton.onClick.AddListener(() => OnSellButtonClicked(FarmEntityName.Cow));
        sellStrawBerryButton.onClick.AddListener(() => OnSellButtonClicked(FarmEntityName.Strawberry));
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        if(Instance == null) _instance = this;

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        sellEntityCanvas.gameObject.SetActive(false);
        var inventory = FindFirstObjectByType<FarmMN>().farmRepository.Load().Inventory;

        sellTomatoText.text = inventory.GetProductCount(FarmEntityName.Tomato).ToString();
        sellBlueberryText.text = inventory.GetProductCount(FarmEntityName.Blueberry).ToString();
        sellCowText.text = inventory.GetProductCount(FarmEntityName.Cow).ToString();
        sellStrawBerryText.text = inventory.GetProductCount(FarmEntityName.Strawberry).ToString();
    }

    private void OnSellButtonClicked(string name)
    {
        bool ishow = (name == FarmEntityName.Tomato && !sellTomatoText.text.Equals("0"))
            || (name == FarmEntityName.Blueberry && !sellBlueberryText.text.Equals("0"))
            || (name == FarmEntityName.Cow && !sellCowText.text.Equals("0"))
            || (name == FarmEntityName.Strawberry && !sellStrawBerryText.text.Equals("0"));

        if (ishow) sellEntityCanvas.Show(name);
    }
}
