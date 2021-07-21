using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTypeSelection : MonoBehaviour, RequiredComponent
{
    public void ConfigureRequiredComponent()
    {
        PersonalizationEventsManager.Instance.AddActionToEvent(PersonalizationEvent.DependenciesLoaded, InitializeBodies);
    }

    void InitializeBodies()
    {
        BodyOptionButton[] buttons = GetComponentsInChildren<BodyOptionButton>();
        int index = 0;
        List<BodyMesh> bodyTypes = BodyMeshController.Instance.GetBodyTypes();
        foreach (BodyOptionButton button in buttons)
        {
            button.Initialize(bodyTypes[index], ChangeBody);
            index++;
        }
    }

    void ChangeBody(BodyMesh bodyType)
    {
        BodyTypePersonalization.Instance.ChangedBody(bodyType);
    }
}
