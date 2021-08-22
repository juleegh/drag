using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubConfiguration : MonoBehaviour
{
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform dancefloor;
    [SerializeField] private CharacterWalking character;
    [SerializeField] private List<Transform> idlePositions;
    [SerializeField] private List<Transform> dancingPositions;

    public void LoadClubConfig()
    {
        LoadNPCs();
        SetPlayerInEntryPoint();
    }

    private void SetPlayerInEntryPoint()
    {
        character.transform.position = entryPoint.position;
        character.PossesPlayer();
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
