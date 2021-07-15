using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embelisher : ColorPicking, IRequiredComponent
{
    private static Embelisher instance;
    public static Embelisher Instance { get { return instance; } }
    public EmbelishingVariables EmbelishingVariables;

    private GameObject preview;
    private Decoration currentlySelected;
    private bool erasing;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        EmbelishingVariables = new EmbelishingVariables();

        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadPreview);
    }

    private void LoadPreview()
    {
        preview = Inventory.Instance.GetOneDecoration();
        EmbelishingVariables.CurrentScale = Vector3.one;
    }

    void Update()
    {
        if (OutfitStepManager.Instance.CurrentOutfitStep != OutfitStep.Outfit)
            return;

        preview.gameObject.SetActive(false);
        erasing = Input.GetKey(KeyCode.Tab);

        if (Input.GetMouseButtonDown(0))
        {
            if (erasing)
                CheckForErasing();
            else
                CheckForPlacement();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            EmbelishingVariables.CurrentRotation -= 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            EmbelishingVariables.CurrentRotation += 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (EmbelishingVariables.CurrentScale.magnitude < 5)
                EmbelishingVariables.CurrentScale *= 1.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (EmbelishingVariables.CurrentScale.magnitude > 1.1f)
                EmbelishingVariables.CurrentScale *= 0.9f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            EmbelishingVariables.mirrored = !EmbelishingVariables.mirrored;
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
        decoration.transform.rotation = Quaternion.LookRotation(hit.normal);
        decoration.transform.Rotate(Vector3.forward * EmbelishingVariables.Rotation, Space.Self);
        if (EmbelishingVariables.mirrored)
            decoration.transform.Rotate(Vector3.up * 180, Space.Self);
        decoration.transform.localScale = EmbelishingVariables.Scale;
        decoration.GetComponent<Decoration>().SetColor(EmbelishingVariables.GetTempColor(currentColor));
        decoration.GetComponent<Decoration>().LoadInfo(Inventory.Instance.CurrentSelected.CodeName, Inventory.Instance.CurrentSelected.Sprite);
    }

    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        EmbelishingVariables.RandomnizeValues();
    }

    private void CheckForPlacement()
    {
        ChangeCurrentlySelected(preview.GetComponent<Decoration>());
        int layer_mask = LayerMask.GetMask("Manniquin");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layer_mask))
        {
            GameObject decoration = Inventory.Instance.GetOneDecoration();
            CreateObjectToHit(decoration, hit);
            EmbelishingVariables.RandomnizeValues();
            decoration.transform.SetParent(PosePerformer.Instance.GetClosestBone(hit.point));
        }
    }

    private void CheckForErasing()
    {
        int layer_mask = LayerMask.GetMask("Decoration");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layer_mask))
        {
            Destroy(hit.transform.gameObject);
        }
    }

    private void PreviewForPlacement()
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("Manniquin");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layer_mask))
        {
            preview.gameObject.SetActive(true);
            CreateObjectToHit(preview, hit);
            ChangeCurrentlySelected(preview.GetComponent<Decoration>());
        }
    }

    private void PreviewForErasing()
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("Decoration");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layer_mask))
        {
            ChangeCurrentlySelected(hit.transform.GetComponent<Decoration>());
        }
    }
}
