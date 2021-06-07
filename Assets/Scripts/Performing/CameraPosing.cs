using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraPosing : MonoBehaviour
{
    private static CameraPosing instance;
    public static CameraPosing Instance { get { return instance; } }

    [SerializeField] private Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void HitPose(PoseType poseType)
    {
        animator.Rebind();
        animator.Play(poseType.ToString());
    }

}
