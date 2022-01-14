using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "Move Action")]
    public class MoveAction : BattleAction
    {
        [SerializeField] private List<Vector2Int> moveDelta;

        public override List<Vector2Int> TargetPositions { get { return moveDelta; } }

        public override void Execute()
        {
            foreach (Vector2Int position in moveDelta)
                BattleGridManager.Instance.CharacterMoved(position);
        }
    }
}
