using UnityEngine;

public class SabotageMove : DanceMove
{
    [SerializeField] private AttackBuff attackBuff;
    public AttackBuff AttackBuff { get { return attackBuff; } }
}