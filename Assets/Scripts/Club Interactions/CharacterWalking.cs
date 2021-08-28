using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalking : MonoBehaviour, RequiredComponent
{
    [SerializeField] private float speed;
    bool isWalking;
    bool isPossesed;

    public void ConfigureRequiredComponent()
    {
        isWalking = false;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.StartPerformance, ReleasePlayer);
    }

    public void PossesPlayer()
    {
        GlobalPlayerManager.Instance.gameObject.SetActive(false);
        GlobalPlayerManager.Instance.transform.SetParent(this.transform);
        GlobalPlayerManager.Instance.transform.localPosition = Vector3.zero;
        GlobalPlayerManager.Instance.transform.eulerAngles = Vector3.zero;
        GlobalPlayerManager.Instance.gameObject.SetActive(true);
        isPossesed = true;
    }

    private void ReleasePlayer()
    {
        isPossesed = false;
        GlobalPlayerManager.Instance.transform.SetParent(null);
        GlobalPlayerManager.Instance.transform.position = Vector3.zero + Vector3.up * 2.2f;
        GlobalPlayerManager.Instance.transform.eulerAngles = Vector3.up * 180;
    }

    private void Update()
    {
        if (!isPossesed)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.LookAt(transform.position + movement);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement.magnitude != 0)
        {
            if (!isWalking)
            {
                isWalking = true;
                PosePerformer.Instance.HitPose(PoseType.Walking);
            }
            PosePerformer.Instance.SetSpeed(movement.magnitude);
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                PosePerformer.Instance.HitPose(PoseType.Idle);
            }
        }
    }
}
