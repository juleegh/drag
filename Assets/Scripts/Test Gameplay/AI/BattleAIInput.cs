using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleAIInput : BattleActionExecuter
    {
        protected static BattleAIInput instance;
        public static BattleAIInput Instance { get { return instance; } }

        private float chanceToDefend = 0.40f;
        private float chanceToSpecial = 0.65f;

        private AIAttackLogic attackLogic;
        private AIDefenseLogic defenseLogic;
        private AIMoveLogic moveLogic;
        private AISpecialLogic specialLogic;

        private bool willDefend;
        private bool willSpecial;
        private bool turnFinished;

        void Awake()
        {
            instance = this;
            attackLogic = new AIAttackLogic(this);
            moveLogic = new AIMoveLogic(this);
            defenseLogic = new AIDefenseLogic(this);
            specialLogic = new AISpecialLogic(this);
            turnFinished = true;
        }

        public void NewTempo()
        {
            if (BattleSectionManager.Instance.IsPlayerTurn || BattleSectionManager.Instance.Opponent.Stats.Health <= 0)
                return;

            if (turnFinished)
            {
                turnFinished = false;
                willDefend = PassesCheck(chanceToDefend);
                willSpecial = PassesCheck(chanceToSpecial);
            }

            if (willDefend && TryToUseDefenseAbility())
            {
                //Debug.LogError("Defended");
            }
            else if (willSpecial && TryToUseSpecialAbility())
            {
                //Debug.LogError("Special");
            }
            else if (TryToGetAway())
            {
                //Debug.LogError("Escaped");
            }
            else if (TryToAttackPlayer())
            {
                //Debug.LogError("Attack");
            }

            if (BattleSectionManager.Instance.TemposRemaining == 1)
            {
                willDefend = false;
                willSpecial = false;
                turnFinished = true;
            }
        }

        private bool TryToUseSpecialAbility()
        {
            BattleAction abilityToUse = specialLogic.PickAbilityToUse();
            if (abilityToUse != null)
            {
                if (specialLogic.SpecialAbilityIsInRange(abilityToUse))
                {
                    if (currentActionType == BattleActionType.Special)
                    {
                        abilityToUse.Execute();
                        willSpecial = false;
                    }
                    else
                    {
                        currentActionType = BattleActionType.Special;
                        BattleGridManager.Instance.UpdatePreview();
                    }
                }
                else
                {
                    if (!moveLogic.CanMoveToSpecial(abilityToUse.TargetDirections))
                    {
                        return false;
                    }

                    if (currentActionType == BattleActionType.Move)
                    {
                        moveLogic.MoveTorwardsSpecialAttack(abilityToUse.TargetDirections);
                    }
                    else
                    {
                        currentActionType = BattleActionType.Move;
                        BattleGridManager.Instance.UpdatePreview();
                    }
                }
                return true;
            }
            return false;
        }

        private bool TryToUseDefenseAbility()
        {
            BattleAction abilityToUse = defenseLogic.PickAbilityToUse();
            if (abilityToUse != null)
            {
                if (currentActionType == BattleActionType.Defend)
                {
                    abilityToUse.Execute();
                    willDefend = false;
                }
                else
                {
                    currentActionType = BattleActionType.Defend;
                    BattleGridManager.Instance.UpdatePreview();
                }
                return true;
            }
            return false;
        }

        private bool TryToGetAway()
        {
            float healthPercentage = BattleSectionManager.Instance.InTurn.Stats.Health / BattleSectionManager.Instance.InTurn.Stats.BaseHealth;
            int turnsLeft = BattleSectionManager.Instance.TemposRemaining;

            if (healthPercentage <= 0.3f && turnsLeft <= BattleSectionManager.Instance.TemposPerPlayer / 4)
            {
                if (currentActionType == BattleActionType.Move)
                    moveLogic.MoveAwayFromPlayer();
                else
                {
                    currentActionType = BattleActionType.Move;
                    BattleGridManager.Instance.UpdatePreview();
                }
                return true;
            }
            return false;
        }

        private bool TryToAttackPlayer()
        {
            if (attackLogic.IsPlayerInAttackRange())
            {
                if (currentActionType == BattleActionType.Attack)
                {
                    attackLogic.AttackPlayer();
                }
                else
                {
                    currentActionType = BattleActionType.Attack;
                    BattleGridManager.Instance.UpdatePreview();
                }
            }
            else
            {
                if (currentActionType != BattleActionType.Move)
                {
                    currentActionType = BattleActionType.Move;
                    BattleGridManager.Instance.UpdatePreview();
                }
                else
                    moveLogic.MoveTorwardsPlayer();
            }

            return true;
        }

        private bool PassesCheck(float valueToCheck)
        {
            return Random.Range(0f, 1f) >= valueToCheck;
        }
    }
}
