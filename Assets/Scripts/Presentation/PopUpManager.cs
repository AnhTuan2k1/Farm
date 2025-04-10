using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public LandPlotPlantCanvas landPlotPlantCanvas;
    public LandPlotPurchaseCanvas landPlotPurchaseCanvas;
    public NofiticationCanvas nofiticationCanvas;

    private static PopUpManager _instance;
    public static PopUpManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<PopUpManager>();
            return _instance;
        }
    }

    private void Start()
    {
        landPlotPlantCanvas.gameObject.SetActive(false);
        landPlotPurchaseCanvas.gameObject.SetActive(false);
        nofiticationCanvas.gameObject.SetActive(false);
    }

    public bool IsShowing()
    {
        return landPlotPlantCanvas.isActiveAndEnabled
                || landPlotPurchaseCanvas.isActiveAndEnabled;
    }

    public void ShowLandPlotPlantCanvas(LandPlotController landPlotController)
    {
        HideAllCanvases();
        landPlotPlantCanvas.Show(landPlotController);
    }

    public void ShowLandPlotPurchaseCanvas(LandPlotController landPlotController)
    {
        HideAllCanvases();
        landPlotPurchaseCanvas.Show(landPlotController);
    }

    public void ShowNotificationCanvas(LandPlotController landPlotController, string content)
    {
        nofiticationCanvas.ShowNotification(landPlotController.gameObject, content);
    }

    private void HideAllCanvases()
    {
        landPlotPlantCanvas.Hide();
        landPlotPurchaseCanvas.Hide();
    }
}
