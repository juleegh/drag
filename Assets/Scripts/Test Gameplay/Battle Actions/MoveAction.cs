using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "Move Action")]
    public class MoveAction : BattleAction
    {
        [SerializeField] private List<Vector2Int> moveDelta;

        public override List<Vector2Int> TargetDirections { get { return moveDelta; } }
        public override BattleActionType ActionType { get { return BattleActionType.Move; } }

        public override void Execute()
        {
            Vector2Int previous = Vector2Int.zero;
            foreach (Vector2Int position in moveDelta)
            {
                bool couldMove = BattleGridManager.Instance.MoveCharacter(position - previous);
                if (!couldMove)
                    break;
                previous = position;
            }
            base.Execute();
            BattleGridManager.Instance.UpdatePreview();
        }

        public override bool WouldHaveEffect()
        {
            if (TargetDirections.Count == 0)
                return false;

            Vector2Int ownerPosition = BattleSectionManager.Instance.InTurn.CurrentPosition;
            return BattleGridManager.Instance.IsValidPosition(TargetDirections[0] + ownerPosition);
        }
    }
}
