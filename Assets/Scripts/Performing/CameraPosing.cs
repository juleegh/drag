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
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TurnAdvanced, TurnAdvanced);
    }

    private void GoToPerformance()
    {
        transform.position = ClubLevelLoader.Instance.CurrentClubConfiguration.PlayerPositions.CameraStagePosition.position;
        transform.eulerAngles = ClubLevelLoader.Instance.CurrentClubConfiguration.PlayerPositions.CameraStagePosition.eulerAngles;
        PosePerformer.Instance.SetSpeed(0.2f);
        HitPose(PoseType.Walking);
    }

    private Transform CurrentTransform
    {
        get { return DanceBattleManager.Instance.IsPlayerTurn ? ClubLevelLoader.Instance.CurrentClubConfiguration.PlayerPositions.CameraStagePosition : ClubLevelLoader.Instance.CurrentClubConfiguration.OpponentPositions.CameraStagePosition; }
    }

    private void StartedPerformance()
    {
        PosePerformer.Instance.SetSpeed(1f);
        PosePerformer.Instance.HitPose(PoseType.Idle);
    }

    private void TurnAdvanced()
    {
        transform.position = CurrentTransform.position;
        transform.eulerAngles = CurrentTransform.eulerAngles;
    }

    public void HitPose(PoseType poseType)
    {
        if (!IsPosing(poseType) && poseType != PoseType.Walking)
            return;
        animator.enabled = true;
        animator.Rebind();
        animator.Play(poseType.ToString());
    }

    private bool IsPosing(PoseType poseType)
    {
        int poseId = (int)poseType;
        return poseId >= 100 && poseId < 200;
    }

    public void Reset()
    {
        animator.StopPlayback();
        animator.enabled = false;
        cameraObject.transform.localPosition = Vector3.zero;
        cameraObject.transform.eulerAngles = Vector3.zero;
    }
}
