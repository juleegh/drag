using UnityEngine;

public class AttackMove : DanceMove
{
    [SerializeField] private AttackBuff attackBuff;
    public AttackBuff AttackBuff { get { return attackBuff; } }
}