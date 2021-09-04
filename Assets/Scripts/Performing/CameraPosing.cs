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
        transform.position = ClubLevelLoader.Instance.CurrentClubConfiguration.CameraStagePosition.position;
        transform.eulerAngles = ClubLevelLoader.Instance.CurrentClubConfiguration.CameraStagePosition.eulerAngles;
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
        if (!IsPosing(poseType) && poseType != PoseType.Walking)
            return;
        animator.Rebind();
        animator.Play(poseType.ToString());
    }

    private bool IsPosing(PoseType poseType)
    {
        int poseId = (int)poseType;
        return poseId >= 100 && poseId < 200;
    }

}
