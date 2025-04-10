using UnityEngine;

public class LandPlotPlantCanvas : MonoBehaviour
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

    public void Blueberry()
    {
        caller.Plant(FarmEntityName.Blueberry, "Crop");
        Hide();
    }

    public void Tomato()
    {
        caller.Plant(FarmEntityName.Tomato, "Crop");
        Hide();
    }

    public void Cow()
    {
        caller.Plant(FarmEntityName.Cow, "Animal");
        Hide();
    }

    public void Strawberry()
    {
        caller.Plant(FarmEntityName.Strawberry, "Strawberry");
        Hide();
    }

}
