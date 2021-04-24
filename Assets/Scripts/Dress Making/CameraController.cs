using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mannequin;
    [SerializeField] private Transform cameraTransform;
    float minFov = 3f;
    float maxFov = 10f;
    float sensitivity = 10f;
    float minUp = -1f;
    float maxUp = 1f;
    float upSensitivity = 0.3f;
    float moveSensitivity = 10f;
 
    void Start()
    {
    }

    void Update () 
    {
        if(Input.GetKey(KeyCode.X))
            MoveY();
        else if(Input.GetKey(KeyCode.C))
            RotateInX();            
        else
            Zoom();
    }

    private void Zoom()
    {
        Vector3 manPosition = mannequin.position;
        manPosition.y = 0f;
        Vector3 cameraPos = cameraTransform.position;
        cameraPos.y = 0f;
        float fov = Vector3.Distance(manPosition, cameraPos); 
        float inputVal = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov += inputVal;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Vector3 newPos = manPosition + cameraTransform.forward * -fov;
        cameraTransform.position = newPos;
        cameraTransform.localPosition = cameraTransform.localPosition - Vector3.up * cameraTransform.localPosition.y;
    }

    private void MoveY()
    {
        float up = Input.GetAxis("Mouse ScrollWheel") * upSensitivity;
        up = Mathf.Clamp(up, minUp, maxUp);
        transform.Translate(transform.up * up);
    }

    private void RotateInX()
    {
        float angle = Input.GetAxis("Mouse ScrollWheel") * moveSensitivity;
        transform.eulerAngles += Vector3.up * angle;
    }
}
