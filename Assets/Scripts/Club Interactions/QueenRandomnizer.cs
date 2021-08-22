using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenRandomnizer : MonoBehaviour
{
    [SerializeField] private WigPreview wigPreview;
    [SerializeField] private BodyPreview bodyPreview;
    [SerializeField] private CharacterPose characterPose;
    [SerializeField] private List<Color> skinTones;

    public void MakeRandomIdleQueen()
    {
        GenerateRandomQueen();
        characterPose.HitPose(GetRandomIdle());
        characterPose.AddOffset();
    }

    public void MakeRandomDancingQueen()
    {
        GenerateRandomQueen();
        characterPose.HitPose(GetRandomDancing());
        characterPose.AddOffset();
    }

    private void GenerateRandomQueen()
    {
        bodyPreview.ChangeBody(GetRandomBody());
        wigPreview.ChangeSelected(GetRandomWig());

        bodyPreview.ChangeSkinColor(GetRandomSkinTone());
        bodyPreview.ChangeClothesColor(GetRandomColor());
        wigPreview.SetCurrentColor(GetRandomColor());
    }

    private BodyMesh GetRandomBody()
    {
        List<BodyMesh> bodyMeshes = BodyMeshController.Instance.BodyMeshes;
        return bodyMeshes[Random.Range(0, bodyMeshes.Count)];
    }

    private WigConfig GetRandomWig()
    {
        List<WigConfig> wigs = WigSelection.Instance.WigList;
        return wigs[Random.Range(0, wigs.Count)];
    }

    private Color GetRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r, g, b, 1f);
    }

    private Color GetRandomSkinTone()
    {
        return skinTones[Random.Range(0, skinTones.Count)];
    }

    private PoseType GetRandomIdle()
    {
        List<PoseType> posesList = new List<PoseType>();
        posesList.Add(PoseType.Club_Idle_1);
        posesList.Add(PoseType.Club_Idle_2);
        posesList.Add(PoseType.Club_Idle_3);
        return posesList[Random.Range(0, posesList.Count)];
    }

    private PoseType GetRandomDancing()
    {
        List<PoseType> posesList = new List<PoseType>();
        posesList.Add(PoseType.Club_Dancing_1);
        posesList.Add(PoseType.Club_Dancing_2);
        posesList.Add(PoseType.Club_Dancing_3);
        posesList.Add(PoseType.Club_Dancing_4);
        posesList.Add(PoseType.Club_Dancing_5);
        posesList.Add(PoseType.Club_Dancing_6);
        return posesList[Random.Range(0, posesList.Count)];
    }
}
