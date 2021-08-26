using UnityEngine;

[CreateAssetMenu(fileName = "Dance Move")]
public class DanceMove : ScriptableObject
{
    [SerializeField] private int staminaRequired;
    [SerializeField] private PoseType poseType;

    public string Identifier { get { return poseType.ToString(); } }
    public int StaminaRequired { get { return staminaRequired; } }
}