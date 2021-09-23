using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClubProperties : MonoBehaviour
{
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform stagePosition;
    [SerializeField] private Transform cameraStagePosition;

    public Transform EntryPoint { get { return entryPoint; } }
    public Transform StagePosition { get { return stagePosition; } }
    public Transform CameraStagePosition { get { return cameraStagePosition; } }
}