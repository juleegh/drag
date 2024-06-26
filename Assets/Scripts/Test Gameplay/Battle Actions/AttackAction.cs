using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "Attack Action")]
    public class AttackAction : BattleAction
    {
        [SerializeField] private List<Vector2Int> attackedPositions;
        [SerializeField] private int damage;

        public override List<Vector2Int> TargetDirections { get { return attackedPositions; } }
        public override BattleActionType ActionType { get { return BattleActionType.Attack; } }

        public override void Execute()
        {
            if (HasEnoughStamina())
            {
                BattleSectionManager.Instance.InTurn.DecreaseStamina(requiredStamina);
                foreach (Vector2Int direction in TargetDirections)
                    BattleGridManager.Instance.CharacterAttacked(BattleSectionManager.Instance.InTurn.CurrentPosition, direction, damage);
                base.Execute();

                Vector2Int previous = Vector2Int.zero;
                foreach (Vector2Int position in TargetDirections)
                {
                    bool couldMove = BattleGridManager.Instance.MoveCharacter(position - previous, true);
                    if (!couldMove)
                        break;
                    previous = position;
                }

                BattleGridManager.Instance.UpdatePreview();
            }
        }

        public override bool WouldHaveEffect()
        {
            return OpponentInTargetPosition();
        }
    }
}
