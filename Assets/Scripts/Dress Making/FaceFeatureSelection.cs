using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceFeatureSelection : ColorPicking, RequiredComponent
{
    [SerializeField] private FacePart facePart;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image preview;

    private int currentIndex;
    private bool ready = false;

    public void ConfigureRequiredComponent()
    {
        ready = true;
        previousButton.onClick.AddListener(Previous);
        nextButton.onClick.AddListener(Next);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, Initialize);
    }

    void Initialize()
    {
        RefreshSelection(MakeupSelection.Instance.GetCurrent(facePart));
    }

    void Previous()
    {
        FaceFeature feature = MakeupSelection.Instance.GetPrevious(facePart);
        RefreshSelection(feature);
    }

    void Next()
    {
        FaceFeature feature = MakeupSelection.Instance.GetNext(facePart);
        RefreshSelection(feature);
    }

    void RefreshSelection(FaceFeature feature)
    {
        preview.sprite = feature.Sprite;
        MakeupManager.Instance.SelectedFeature(feature);
    }

    public override void SetCurrentColor(Color color)
    {
        if (!ready)
            return;

        base.SetCurrentColor(color);
        preview.color = color;
        MakeupSelection.Instance.UpdatedColor(facePart, color);
        MakeupManager.Instance.SelectedColor(facePart, color);
    }
}
