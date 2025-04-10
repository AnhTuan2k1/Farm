using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LandPlotController : MonoBehaviour
{
    [SerializeField] private LandPlot landPlot;
    [SerializeField] private int landIndex;
    [SerializeField] private TMP_Text landStatus;
    [SerializeField] private Slider progress_slider;
    [SerializeField] private TMP_Text progress_Text;
    private FarmMN farmMN;
    private bool update;

    private SowSeedUseCase sowSeedUseCase;
    private ExpandLandUseCase expandLandUseCase;

    public void Init(FarmMN farmMN,
        SowSeedUseCase sowSeedUseCase,
        ExpandLandUseCase expandLandUseCase,
        LandPlot landPlot, int i)
    {
        this.sowSeedUseCase = sowSeedUseCase;
        this.expandLandUseCase = expandLandUseCase;
        this.landPlot = landPlot;
        this.farmMN = farmMN;
        landIndex = i;

        update = true;
        SetProgress(0);
    }

    public void Plant(string name, string type)
    {
        var msg = sowSeedUseCase.Execute(name, type, landIndex);
        if (msg == null)
        {
            update = true;
            UpdateUI();
            Debug.Log($"✅ Đã trồng {name} tại ô {landIndex}");
        }
        else
        {
            Debug.LogWarning($"Không thể trồng {name} tại ô {landIndex}");
            PopUpManager.Instance.ShowNotificationCanvas(this, msg);
        }
    }

    public void PurchaseLandPlot()
    {
        var msg = expandLandUseCase.Execute(landIndex);
        if (msg == null)
        {
            update = true;
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("❌ Không thể mở khóa ô đất");
            PopUpManager.Instance.ShowNotificationCanvas(this, msg);
        }
    }

    public void UpdateUI()
    {
        if (update)
        {
            update = false;


            if (landPlot.Occupant != null)
            {
                update = true;
                SetProgress(landPlot.Progress());
                if (landPlot.IsPreparing())
                {
                    landStatus.text = $"{landPlot.Occupant.Config.Name}\n{LandPlotStatus.PREPARING}";
                    GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else if (landPlot.IsProducing())
                {
                    landStatus.text 
                        = $"{landPlot.Occupant.Config.Name}\n{LandPlotStatus.PRODUCING}\n {landPlot.ProgressTimeStringhhmmss()}";
                    GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else if (landPlot.WaittingForHarvesting())
                {
                    landStatus.text = $"{landPlot.Occupant.Config.Name}\n{LandPlotStatus.WAITTING_FOR_HARVERSTING}\n{landPlot.ProgressTimeStringhhmmss()}";
                    GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else if (landPlot.IsHarvesting())
                {
                    landStatus.text = $"{landPlot.Occupant.Config.Name}\n{LandPlotStatus.HARVERSTING}";
                    GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else if (landPlot.Occupant.IsHarvested && landPlot.IsHarvestingDone())
                {
                    farmMN.farmRepository.Load().Harvest(landIndex);
                    update = true;
                    UpdateUI();
                } else {
                    landStatus.text = $"{landPlot.Occupant.Config.Name}\n{LandPlotStatus.DIE}";
                    GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }

            else if (landPlot.IsUnlocked)
            {
                landStatus.text = LandPlotStatus.IDLE;
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
                progress_slider.gameObject.SetActive(false);
            }
            else
            {
                landStatus.text = LandPlotStatus.LOCKED;
                Color color = GetComponentInChildren<SpriteRenderer>().color;
                GetComponentInChildren<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.7f);
            }
        }


    }

    private class LandPlotStatus
    {
        public static readonly string PREPARING = "worker is preparing";
        public static readonly string PRODUCING = "is in producing";
        public static readonly string WAITTING_FOR_HARVERSTING = "waitting for harvesting";
        public static readonly string HARVERSTING = "worker is harvesting";
        public static readonly string LOCKED = "tap to unlock land";
        public static readonly string IDLE = "idle land";
        public static readonly string DIE = "died";
    }

    public void OnClick()
    {
        if (landPlot.IsUnlocked)
        {
            if (landPlot.Occupant == null) PopUpManager.Instance.ShowLandPlotPlantCanvas(this);
            else if (landPlot.Occupant.CanHarvest())
            {
                farmMN.farmRepository.Load().PrepareForHarvest(landIndex);
                update = true;
                UpdateUI();
            } else if (landPlot.Occupant.IsDead(DateTime.Now))
            {
                landPlot.AssignEntity(null);
                update = true;
                UpdateUI();
            }
        }
        else PopUpManager.Instance.ShowLandPlotPurchaseCanvas(this);
    }

    public void SetProgress(float value)
    {
        progress_slider.value = value;
        progress_Text.text = value == 1 ? "done" : $"{((int)(value * 10000))/100f}%";

        if (progress_slider.value <= 0.000001f)
        {
            progress_slider.gameObject.SetActive(false);
        }
        else
        {
            progress_slider.gameObject.SetActive(true);
        }
    }
}
