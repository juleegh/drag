using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosePerformer : MonoBehaviour
{
    private static PosePerformer instance;
    public static PosePerformer Instance { get { return instance; } }

    [SerializeField] private Animator animator;
    [SerializeField] private BodyPoseSettings settings;

    private List<Transform> bones;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            GameObject[] theBones = GameObject.FindGameObjectsWithTag("Bone");
            bones = new List<Transform>();
            foreach (GameObject bone in theBones)
            {
                bones.Add(bone.transform);
            }
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

    public Transform GetClosestBone(Vector3 pos)
    {
        Transform closest = bones[0];
        float distance = Vector3.Distance(pos, closest.position);

        foreach (Transform bone in bones)
        {
            if (Vector3.Distance(bone.position, pos) < distance)
            {
                closest = bone;
                distance = Vector3.Distance(pos, closest.position);
            }
        }

        return closest;
    }
}
