using UnityEngine;

public class DanceMove : ScriptableObject
{
    [SerializeField] private int staminaRequired;
    [SerializeField] private PoseType poseType;
    [SerializeField] private int score;

    public string Identifier { get { return poseType.ToString(); } }
    public int StaminaRequired { get { return staminaRequired; } }
    public PoseType PoseType { get { return poseType; } }
    public int Score { get { return score; } }

    public void MakeEmpty()
    {
        staminaRequired = 0;
        poseType = PoseType.Idle;
    }
}