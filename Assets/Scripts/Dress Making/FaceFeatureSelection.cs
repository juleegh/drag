using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceFeatureSelection : ColorPicking
{
    [SerializeField] private List<FaceFeature> options;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image preview;

    private int currentIndex;

    void Awake()
    {
        previousButton.onClick.AddListener(Previous);
        nextButton.onClick.AddListener(Next);
    }

    void Start()
    {
        RefreshSelection();
    }

    void Previous()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = options.Count - 1;
        RefreshSelection();
    }

    void Next()
    {
        currentIndex++;
        if (currentIndex == options.Count)
            currentIndex = 0;
        RefreshSelection();
    }

    void RefreshSelection()
    {
        preview.sprite = options[currentIndex].Sprite;
        MakeupManager.Instance.SelectedFeature(options[currentIndex]);
    }

    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        preview.color = color;
        MakeupManager.Instance.SelectedColor(options[currentIndex], color);
    }
}
