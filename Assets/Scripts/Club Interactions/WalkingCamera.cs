using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCamera : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Vector3 distanceFromPlayer;
    [SerializeField] private Vector3 followingAngle;
    [SerializeField] private float speed;
    [SerializeField] private CharacterWalking characterWalking;
    private bool followingPlayer;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, StartFollowingPlayer);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.LeftDanceFloor, StartFollowingPlayer);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.EnteredTheDanceFloor, StopFollowingPlayer);
    }

    private void StartFollowingPlayer()
    {
        transform.position = characterWalking.transform.position - distanceFromPlayer;
        transform.eulerAngles = followingAngle;
        followingPlayer = true;
    }

    private void StopFollowingPlayer()
    {
        followingPlayer = false;
    }

    private void Update()
    {
        if (followingPlayer)
        {
            Vector3 expectedPosition = characterWalking.transform.position - distanceFromPlayer;
            transform.position = Vector3.Lerp(transform.position, expectedPosition, Time.deltaTime * speed);
            transform.eulerAngles = followingAngle;

        }
    }
}
