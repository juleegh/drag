using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embelisher : ColorPicking, RequiredComponent
{
    private static Embelisher instance;
    public static Embelisher Instance { get { return instance; } }
    public EmbelishingVariables EmbelishingVariables;

    private GameObject preview;
    private Decoration currentlySelected;
    public bool erasing;
    private float clickDelay = 0f;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        EmbelishingVariables = new EmbelishingVariables();

        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadPreview);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.FinishedOutfit, CleanPreview);
    }

    private void LoadPreview()
    {
        preview = Inventory.Instance.GetOneDecoration();
        EmbelishingVariables.CurrentScale = Vector3.one;
        preview.gameObject.SetActive(false);
    }

    private void CleanPreview()
    {
        Inventory.Instance.ReturnDecoration(preview);
        preview = null;
    }

    void Update()
    {
        if (instance == null || OutfitStepManager.Instance.CurrentOutfitStep != OutfitStep.Outfit)
            return;

        preview.gameObject.SetActive(false);
        if (clickDelay > 0)
            clickDelay -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (erasing)
                CheckForErasing();
            else if (clickDelay <= 0)
                CheckForPlacement();
        }
        else
        {
            ChangeCurrentlySelected(null);
            if (erasing)
                PreviewForErasing();
            else
                PreviewForPlacement();
        }
    }

    private void ChangeCurrentlySelected(Decoration next)
    {
        if (currentlySelected == next)
            return;
        if (currentlySelected != null)
            currentlySelected.PreviewColor(false);
        currentlySelected = next;
        if (currentlySelected != null)
            currentlySelected.PreviewColor(true);
    }

    private void CreateObjectToHit(GameObject decoration, RaycastHit hit)
    {
        decoration.transform.position = hit.point + hit.normal * 0.01f;
        decoration.transform.rotation = Quaternion.LookRotation(-hit.normal);
        decoration.transform.Rotate(Vector3.forward * EmbelishingVariables.Rotation, Space.Self);
        if (EmbelishingVariables.Mirrored)
            decoration.transform.Rotate(Vector3.up * 180, Space.Self);
        decoration.transform.localScale = EmbelishingVariables.Scale;
        decoration.GetComponent<Decoration>().SetColor(EmbelishingVariables.GetTempColor());
        decoration.GetComponent<Decoration>().LoadInfo(Inventory.Instance.CurrentSelected.CodeName, Inventory.Instance.CurrentSelected.Sprite);
    }

    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        if (EmbelishingVariables != null)
        {
            EmbelishingVariables.CurrentColor = color;
            EmbelishingVariables.RandomnizeValues();
        }
    }

    private void CheckForPlacement()
    {
        ChangeCurrentlySelected(preview.GetComponent<Decoration>());
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Manniquin"))
        {
            GameObject decoration = Inventory.Instance.GetOneDecoration();
            CreateObjectToHit(decoration, hit);
            EmbelishingVariables.RandomnizeValues();
            decoration.transform.SetParent(PosePerformer.Instance.GetClosestBone(hit.point));
            clickDelay = 0.05f;
            TimeManager.Instance.AdvanceHour(0.05f);
        }
    }

    private void CheckForErasing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Decoration"))
        {
            Inventory.Instance.ReturnDecoration(hit.transform.gameObject);
        }
    }

    private void PreviewForPlacement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Manniquin"))
        {
            preview.gameObject.SetActive(true);
            CreateObjectToHit(preview, hit);
            ChangeCurrentlySelected(preview.GetComponent<Decoration>());
        }
    }

    private void PreviewForErasing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Decoration"))
        {
            ChangeCurrentlySelected(hit.transform.GetComponent<Decoration>());
        }
    }
}
