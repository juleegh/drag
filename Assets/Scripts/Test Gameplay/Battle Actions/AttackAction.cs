using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "Attack Action")]
    public class AttackAction : BattleAction
    {
        [SerializeField] private List<Vector2Int> attackedPositions;
        [SerializeField] private float damage;

        public override List<Vector2Int> TargetPositions { get { return attackedPositions; } }
        public override void Execute()
        {
            foreach (Vector2Int position in attackedPositions)
                BattleGridManager.Instance.CharacterAttacked(position, damage);
        }
    }
}
