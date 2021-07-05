using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTypeSelection : MonoBehaviour
{
    [SerializeField] private List<BodyType> bodyTypes;

    void Start()
    {
        BodyOptionButton[] buttons = GetComponentsInChildren<BodyOptionButton>();
        int index = 0;
        foreach (BodyOptionButton button in buttons)
        {
            button.Initialize(bodyTypes[index], ChangeBody);
            index++;
        }
    }

    void ChangeBody(BodyType bodyType)
    {
        BodyPersonalization.Instance.ChangedBody(bodyType);
    }
}
