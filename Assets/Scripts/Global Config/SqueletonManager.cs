using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueletonManager : MonoBehaviour, GlobalComponent
{
    private static SqueletonManager instance;
    public static SqueletonManager Instance { get { return instance; } }

    private List<Transform> bones;

    public void ConfigureRequiredComponent()
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
