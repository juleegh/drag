using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosePerformer : MonoBehaviour
{
    private static PosePerformer instance;
    public static PosePerformer Instance { get { return instance; } }

    [SerializeField] private Animator bodyAnimator;

    private List<Transform> bones;
    private bool idle = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GlobalPlayerManager.Instance.GoToPerforming();
        }
        /*
                if (Input.GetKeyDown(KeyCode.H))
                    HitPose(RandomPose());
                    */
    }

    private PoseType RandomPose()
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

    public void HitPose(PoseType poseType)
    {
        bodyAnimator.Rebind();
        bodyAnimator.Play(poseType.ToString());

        if (CameraPosing.Instance != null) CameraPosing.Instance.HitPose(poseType);
        idle = false;
    }

    public void SetTempo(float tempo)
    {
        idle = true;
        bodyAnimator.speed = 1 / tempo;
    }

    public void HitTempo()
    {
        if (idle)
        {
            bodyAnimator.Play("Idle");
        }
    }

    private bool AnimatorIsPlaying()
    {
        return bodyAnimator.GetCurrentAnimatorStateInfo(0).length >
               bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
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
