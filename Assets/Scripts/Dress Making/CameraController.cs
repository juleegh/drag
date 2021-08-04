using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, RequiredComponent
{
    [SerializeField] private float cameraSpeed = 1f;
    [SerializeField] float minFov = 0.5f;
    [SerializeField] float maxFov = 2f;
    [SerializeField] float sensitivity;
    [SerializeField] float maxAngle;
    float minUp = -1f;
    float maxUp = 1f;
    float upSensitivity = 0.3f;
    float moveSensitivity = 10f;
    private Transform mannequin { get { return GlobalPlayerManager.Instance != null ? GlobalPlayerManager.Instance.transform : null; } }

    private Vector3 basePosition;
    private Vector3 baseRotation;
    bool ready = false;

    public void ConfigureRequiredComponent()
    {
        basePosition = transform.localPosition;
        baseRotation = transform.localEulerAngles;
        ready = true;
    }

    void Update()
    {
        //if (!ready || OutfitStepManager.Instance.CurrentOutfitStep != OutfitStep.Outfit)
        if (!ready)
            return;

        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
            MoveHorizontal(Input.GetAxis("Horizontal"), cameraSpeed * Time.deltaTime);

        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            MoveVertical(Input.GetAxis("Vertical"), cameraSpeed * Time.deltaTime);

        if (Input.GetMouseButton(2))
            RotateWithMouse();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            Zoom();
    }

    public void ClearValues()
    {
        transform.localPosition = basePosition;
        transform.localEulerAngles = baseRotation;
    }

    private void Zoom()
    {

        Vector3 manPosition = mannequin.position;
        manPosition.y = 0f;
        Vector3 cameraPos = transform.position;
        cameraPos.y = 0f;
        float fov = Vector3.Distance(manPosition, cameraPos);

        float deltaTime = Time.deltaTime;
        float inputVal = Input.GetAxis("Mouse ScrollWheel") * 100 * sensitivity;

        if (inputVal < 0 && fov + inputVal * deltaTime <= maxFov)
            MoveForward(inputVal, deltaTime * cameraSpeed);
        else if (inputVal > 0 && fov + inputVal * deltaTime >= minFov)
            MoveForward(inputVal, deltaTime * cameraSpeed);
    }

    private void MoveHorizontal(float direction, float time)
    {
        Vector3 right = transform.worldToLocalMatrix.MultiplyVector(transform.right);
        transform.Translate(right * direction * time, Space.Self);
    }

    private void MoveVertical(float direction, float time)
    {
        Vector3 up = transform.worldToLocalMatrix.MultiplyVector(transform.up);
        transform.Translate(up * direction * time, Space.World);
    }

    private void MoveForward(float direction, float time)
    {
        Vector3 forward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        transform.Translate(forward * direction * time, Space.Self);
    }

    private void RotateWithMouse()
    {
        Vector2 currentRotation = new Vector2(transform.eulerAngles.y, transform.eulerAngles.x);

        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
}
