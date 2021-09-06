using UnityEngine;

public class DanceMove : ScriptableObject
{
    [SerializeField] private int staminaRequired;
    [SerializeField] private PoseType poseType;

    public string Identifier { get { return poseType.ToString(); } }
    public int StaminaRequired { get { return staminaRequired; } }
    public PoseType PoseType { get { return poseType; } }

    public void MakeEmpty()
    {
        staminaRequired = 0;
        poseType = PoseType.Idle;
    }
}