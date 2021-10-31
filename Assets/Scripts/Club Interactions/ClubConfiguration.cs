using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubConfiguration : MonoBehaviour
{
    [SerializeField] private CharacterClubProperties playerPositions;
    [SerializeField] private CharacterClubProperties opponentPositions;
    [SerializeField] private Transform opponent;
    [SerializeField] private List<Transform> idlePositions;
    [SerializeField] private List<Transform> dancingPositions;

    public CharacterClubProperties PlayerPositions { get { return playerPositions; } }
    public CharacterClubProperties OpponentPositions { get { return opponentPositions; } }

    public void LoadClubConfig()
    {
        LoadNPCs();
        SetPlayerInEntryPoint();
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.EnteredTheDanceFloor, MoveOpponentToDanceFloor);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.LeftDanceFloor, SetOpponentOnPosition);
    }

    private void MoveOpponentToDanceFloor()
    {
        opponent.position = opponentPositions.StagePosition.position;
        opponent.eulerAngles = opponentPositions.StagePosition.eulerAngles;
    }

    private void SetOpponentOnPosition()
    {
        opponent.position = opponentPositions.EntryPoint.position;
        opponent.eulerAngles = opponentPositions.EntryPoint.eulerAngles;
        OpponentPosePerformer.Instance.HitPose(PoseType.Idle);
    }

    public void SetPlayerInPosition()
    {
        SetPlayerInEntryPoint();
    }

    private void SetPlayerInEntryPoint()
    {
        CharacterWalking.Instance.PlacePlayerForWalking(playerPositions.EntryPoint.position, Quaternion.identity);
        SetOpponentOnPosition();
    }

    private void LoadNPCs()
    {
        foreach (Transform idlePos in idlePositions)
        {
            QueenRandomnizer queen = ClubLevelLoader.Instance.NPCPool.GetObject().GetComponent<QueenRandomnizer>();
            queen.MakeRandomIdleQueen();
            queen.transform.SetParent(idlePos);
            queen.transform.localPosition = Vector3.zero;
            queen.transform.localEulerAngles = Vector3.zero;
        }

        foreach (Transform dancingPos in dancingPositions)
        {
            QueenRandomnizer queen = ClubLevelLoader.Instance.NPCPool.GetObject().GetComponent<QueenRandomnizer>();
            queen.MakeRandomDancingQueen();
            queen.transform.SetParent(dancingPos);
            queen.transform.localPosition = Vector3.zero;
            queen.transform.localEulerAngles = Vector3.zero;
        }
    }
}
