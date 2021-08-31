using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraPosing : MonoBehaviour, RequiredComponent
{
    private static CameraPosing instance;
    public static CameraPosing Instance { get { return instance; } }

    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraObject;

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
        {
            instance = this;
        }
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.EnteredTheDanceFloor, GoToPerformance);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.StartPerformance, StartedPerformance);
    }

    private void GoToPerformance()
    {
        transform.position = new Vector3(0, 2.2f, 0);
        transform.eulerAngles = Vector3.zero;
        PosePerformer.Instance.SetSpeed(0.2f);
        HitPose(PoseType.Walking);
    }

    private void StartedPerformance()
    {
        PosePerformer.Instance.SetSpeed(1f);
        HitPose(PoseType.Idle);
    }

    public void HitPose(PoseType poseType)
    {
        animator.Rebind();
        animator.Play(poseType.ToString());
    }

}
