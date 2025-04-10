using UnityEngine;

public class LandPlotPurchaseCanvas : MonoBehaviour
{
    public LandPlotController caller;

    public void Show(LandPlotController landPlotController)
    {
        caller = landPlotController;
        gameObject.SetActive(true);
        gameObject.transform.position = caller.transform.position;
    }

    public void Hide()
    {
        caller = null;
        gameObject.SetActive(false);
    }

    public void Purchase()
    {
        caller.PurchaseLandPlot();
        Hide();
    }

    public void CancelPurchase()
    {
        Hide();
    }
}
