using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalking : MonoBehaviour, RequiredComponent
{
    private static CharacterWalking instance;
    public static CharacterWalking Instance { get { return instance; } }

    [SerializeField] private float speed;
    bool isWalking;
    bool isPossesed;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        isWalking = false;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.EnteredTheDanceFloor, ReleasePlayer);
    }

    private void PossesPlayer()
    {
        GlobalPlayerManager.Instance.gameObject.SetActive(false);
        GlobalPlayerManager.Instance.transform.SetParent(this.transform);
        GlobalPlayerManager.Instance.transform.localPosition = Vector3.zero;
        GlobalPlayerManager.Instance.transform.eulerAngles = Vector3.zero;
        GlobalPlayerManager.Instance.gameObject.SetActive(true);
        isPossesed = true;
    }

    public void PlacePlayerForWalking(Vector3 position, Quaternion rotation)
    {
        Debug.LogError(1);
        PossesPlayer();
        transform.position = position;
        transform.rotation = rotation;
        WalkingCamera.Instance.StartFollowingPlayer();
    }

    private void ReleasePlayer()
    {
        Debug.LogError(0);
        isPossesed = false;
        GlobalPlayerManager.Instance.transform.SetParent(null);
        WalkingCamera.Instance.StopFollowingPlayer();
        GlobalPlayerManager.Instance.transform.position = ClubLevelLoader.Instance.CurrentClubConfiguration.PlayerPositions.StagePosition.position;
        GlobalPlayerManager.Instance.transform.eulerAngles = ClubLevelLoader.Instance.CurrentClubConfiguration.PlayerPositions.StagePosition.eulerAngles;
    }

    private void Update()
    {
        if (!isPossesed || DialogSystemController.Instance.IsInteracting)
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
