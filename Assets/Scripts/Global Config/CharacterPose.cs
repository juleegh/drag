using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPose : MonoBehaviour
{
    protected Animator bodyAnimator;

    void Awake()
    {
        bodyAnimator = GetComponent<Animator>();
    }

    protected PoseType RandomPose()
    {
        List<PoseType> poses = new List<PoseType>();
        poses.Add(PoseType.Cobra);
        poses.Add(PoseType.Egiptian);
        poses.Add(PoseType.Face_Cover);
        poses.Add(PoseType.Hand_Up);
        poses.Add(PoseType.Hands_Hips);
        poses.Add(PoseType.Knee_Down);
        poses.Add(PoseType.Muscle_Up);
        poses.Add(PoseType.Schwazeneger);
        poses.Add(PoseType.Squat_Like);
        poses.Add(PoseType.Tiger);
        poses.Add(PoseType.Vogue);

        return poses[Random.Range(0, poses.Count)];
    }

    public void AddOffset()
    {
        bodyAnimator.SetFloat("offset", Random.Range(0f, 0.7f));
    }

    public void SetSpeed(float speed)
    {
        bodyAnimator.SetFloat("speed", speed);
    }

    public void HitPose(PoseType poseType)
    {
        bodyAnimator.Rebind();
        bodyAnimator.Play(poseType.ToString());

        if (CameraPosing.Instance != null && IsPosing(poseType)) CameraPosing.Instance.HitPose(poseType);
    }

    protected bool AnimatorIsPlaying()
    {
        return bodyAnimator.GetCurrentAnimatorStateInfo(0).length >
               bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private bool IsPosing(PoseType poseType)
    {
        int poseId = (int)poseType;
        return poseId >= 100 && poseId < 200;
    }
}
