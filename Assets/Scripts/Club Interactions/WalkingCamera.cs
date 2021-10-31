using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCamera : MonoBehaviour, RequiredComponent
{
    private static WalkingCamera instance;
    public static WalkingCamera Instance { get { return instance; } }

    [SerializeField] private Vector3 distanceFromPlayer;
    [SerializeField] private Vector3 followingAngle;
    [SerializeField] private float speed;
    private bool followingPlayer;

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public void StartFollowingPlayer()
    {
        CameraPosing.Instance.Reset();
        transform.position = CharacterWalking.Instance.transform.position - distanceFromPlayer;
        transform.eulerAngles = followingAngle;
        followingPlayer = true;
    }

    public void StopFollowingPlayer()
    {
        followingPlayer = false;
    }

    private void Update()
    {
        if (followingPlayer)
        {

            Vector3 expectedPosition = CharacterWalking.Instance.transform.position - distanceFromPlayer;
            transform.position = Vector3.Lerp(transform.position, expectedPosition, Time.deltaTime * speed);
            transform.eulerAngles = followingAngle;
        }
    }
}
