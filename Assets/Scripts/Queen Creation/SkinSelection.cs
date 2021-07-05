using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelection : MonoBehaviour
{
    [SerializeField] private List<SkinType> skinTypes;

    void Start()
    {
        SkinOptionButton[] buttons = GetComponentsInChildren<SkinOptionButton>();
        int index = 0;
        foreach (SkinOptionButton button in buttons)
        {
            button.Initialize(skinTypes[index], ChangeSkin);
            index++;
        }
    }

    void ChangeSkin(SkinType skinType)
    {
        BodyPersonalization.Instance.ChangeColor(skinType);
    }
}
