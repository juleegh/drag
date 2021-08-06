using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTypePersonalization : MonoBehaviour, RequiredComponent
{
    private static BodyTypePersonalization instance;
    public static BodyTypePersonalization Instance { get { return instance; } }
    private BodyMesh selectedBody;

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
        {
            instance = this;
            selectedBody = BodyMeshController.Instance.GetBodyTypes()[0];
        }
    }

    public void ChangedBody(BodyMesh bodyType)
    {
        selectedBody = bodyType;
        BodyMeshController.Instance.ChangeBody(bodyType);
    }

    public void TryToSave(string dragName)
    {
        SaveInfo(dragName);
        GlobalPlayerManager.Instance.GoToDragging();
    }

    private void SaveInfo(string dragName)
    {
        Color skin = BodyMeshController.Instance.SkinColor;
        string skinColor = skin.r + "," + skin.g + "," + skin.b;
        string[] bodyTypeInfo = selectedBody.name.Split('_');
        PlayerPrefs.SetString("Queen_Skin", skinColor);
        PlayerPrefs.SetString("Queen_Body", bodyTypeInfo[0] + "_" + bodyTypeInfo[1]);
        BodyMeshController.Instance.LoadOutfitsByPlayer();
        PlayerPrefs.SetString("Queen_Name", dragName);
    }
}
