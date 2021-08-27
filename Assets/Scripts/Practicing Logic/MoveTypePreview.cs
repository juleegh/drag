using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveTypePreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveTitle;
    [SerializeField] private GameObject highlight;

    public void UpdateMoveName(string title)
    {
        moveTitle.text = title;
    }

    public void SetEmpty()
    {
        moveTitle.text = "";
        ToggleSelected(false);
    }

    public void ToggleSelected(bool visible)
    {
        highlight.SetActive(visible);
    }
}
