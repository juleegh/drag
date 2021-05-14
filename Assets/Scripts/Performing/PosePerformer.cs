using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosePerformer : MonoBehaviour
{
    private static PosePerformer instance;
    public static PosePerformer Instance { get { return instance; } }

    [SerializeField] private Animator animator;
    [SerializeField] private BodyPoseSettings settings;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void HitPose(PoseType poseType)
    {
        animator.Play(settings.Poses[poseType]);
    }
}
