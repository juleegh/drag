using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmbelisherTools : MonoBehaviour
{
    [SerializeField] private Button drawBtn;
    [SerializeField] private Button eraserBtn;
    private bool currentErasing;

    void Awake()
    {
        drawBtn.onClick.AddListener(() => { ToggleMode(false); });
        eraserBtn.onClick.AddListener(() => { ToggleMode(true); });
        drawBtn.image.color = Color.gray;
        currentErasing = false;
    }

    void OnEnable()
    {
        if (Embelisher.Instance != null)
        {
            ToggleMode(Embelisher.Instance.erasing);
        }

    }

    void Update()
    {
        if (Embelisher.Instance == null)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleMode(!currentErasing);
    }

    private void ToggleMode(bool erasing)
    {
        currentErasing = erasing;
        Embelisher.Instance.erasing = erasing;
        drawBtn.image.color = !erasing ? Color.gray : Color.white;
        eraserBtn.image.color = erasing ? Color.gray : Color.white;
    }
}