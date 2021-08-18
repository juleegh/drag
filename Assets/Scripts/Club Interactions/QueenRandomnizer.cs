using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenRandomnizer : MonoBehaviour, RequiredComponent
{
    [SerializeField] private WigPreview wigPreview;
    [SerializeField] private BodyPreview bodyPreview;
    [SerializeField] private List<Color> skinTones;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, GenerateRandomQueen);
    }

    private void GenerateRandomQueen()
    {
        bodyPreview.ChangeBody(GetRandomBody());
        wigPreview.ChangeSelected(GetRandomWig());

        bodyPreview.ChangeSkinColor(GetRandomSkinTone());
        bodyPreview.ChangeClothesColor(GetRandomColor());
        wigPreview.SetCurrentColor(GetRandomColor());
    }

    private BodyMesh GetRandomBody()
    {
        List<BodyMesh> bodyMeshes = BodyMeshController.Instance.BodyMeshes;
        return bodyMeshes[Random.Range(0, bodyMeshes.Count)];
    }

    private WigConfig GetRandomWig()
    {
        List<WigConfig> wigs = WigSelection.Instance.WigList;
        return wigs[Random.Range(0, wigs.Count)];
    }

    private Color GetRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r, g, b, 1f);
    }

    private Color GetRandomSkinTone()
    {
        return skinTones[Random.Range(0, skinTones.Count)];
    }
}
