using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmbelisherTools : MonoBehaviour
{
    [SerializeField] private Button drawBtn;
    [SerializeField] private Button eraserBtn;

    void Awake()
    {
        drawBtn.onClick.AddListener(() => { ToggleMode(false); });
        eraserBtn.onClick.AddListener(() => { ToggleMode(true); });
        drawBtn.image.color = Color.gray;
    }

    private void ToggleMode(bool erasing)
    {
        Embelisher.Instance.erasing = erasing;
        drawBtn.image.color = !erasing ? Color.gray : Color.white;
        eraserBtn.image.color = erasing ? Color.gray : Color.white;
    }
}