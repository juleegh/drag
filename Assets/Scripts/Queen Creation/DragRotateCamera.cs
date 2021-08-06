using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private bool isRotating;
    private Vector3 previousPos;

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isRotating)
            {
                isRotating = true;
                previousPos = Input.mousePosition;
            }
            else
            {
                float xDelta = previousPos.x - Input.mousePosition.x;
                GlobalPlayerManager.Instance.transform.eulerAngles += transform.up * rotationSpeed * xDelta;
                previousPos = Input.mousePosition;
            }
        }
        else
        {
            isRotating = false;
        }
    }
}
