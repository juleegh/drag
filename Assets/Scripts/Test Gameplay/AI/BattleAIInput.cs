using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public enum AIDecision
    {
        UseSpecialAbility,
        UseDefenseAbility,
        MoveToUseSpecial,
        Escape,
        MoveToUseAttack,
        Attack,
    }

    public class BattleAIInput : BattleActionExecuter
    {
        protected static BattleAIInput instance;
        public static BattleAIInput Instance { get { return instance; } }

        private float chanceToDefend = 0.40f;
        private float chanceToSpecial = 0.65f;
        private float minWaitTime = 0.3f;
        private float maxWaitTime = 0.85f;

        private AIAttackLogic attackLogic;
        private AIDefenseLogic defenseLogic;
        private AIMoveLogic moveLogic;
        private AISpecialLogic specialLogic;

        private bool willDefend;
        private bool willSpecial;
        private bool turnFinished;
        [SerializeField] private AIDecision currentDecision;
        BattleActionType chosenActionType;

        void Awake()
        {
            instance = this;
            attackLogic = new AIAttackLogic(this);
            moveLogic = new AIMoveLogic(this);
            defenseLogic = new AIDefenseLogic(this);
            specialLogic = new AISpecialLogic(this);
            turnFinished = true;
        }

        void Start()
        {
            moveLogic.Initialize();
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
                PickAbilityType();
                return;
            }

            BattleAction abilityToUse;
            switch (currentDecision)
            {
                case AIDecision.MoveToUseSpecial:
                    abilityToUse = specialLogic.PickAbilityToUse();
                    moveLogic.MoveTorwardsSpecialAttack(abilityToUse.TargetDirections);
                    break;
                case AIDecision.UseSpecialAbility:
                    abilityToUse = specialLogic.PickAbilityToUse();
                    abilityToUse.Execute();
                    willSpecial = false;
                    break;
                case AIDecision.UseDefenseAbility:
                    abilityToUse = defenseLogic.PickAbilityToUse();
                    abilityToUse.Execute();
                    willDefend = false;
                    break;
                case AIDecision.MoveToUseAttack:
                    moveLogic.MoveTorwardsPlayer();
                    break;
                case AIDecision.Attack:
                    attackLogic.AttackPlayer();
                    break;
                case AIDecision.Escape:
                    moveLogic.MoveAwayFromPlayer();
                    break;
            }

            if (BattleSectionManager.Instance.TemposRemaining == 1)
            {
                willDefend = false;
                willSpecial = false;
                turnFinished = true;
            }

            PickAbilityType();
        }

        private void PickAbilityType()
        {
            BattleAction specialAbilityToUse = specialLogic.PickAbilityToUse();
            BattleAction defenseAbilityToUse = defenseLogic.PickAbilityToUse();

            if (willDefend && BattleSectionManager.Instance.TemposRemaining <= 3 && defenseAbilityToUse != null)
            {
                chosenActionType = BattleActionType.Defend;
                currentDecision = AIDecision.UseDefenseAbility;
                Decide();
                return;
            }

            if (willSpecial && specialAbilityToUse != null)
            {
                if (specialLogic.SpecialAbilityIsInRange(specialAbilityToUse))
                {
                    chosenActionType = BattleActionType.Special;
                    currentDecision = AIDecision.UseSpecialAbility;
                    Decide();
                    return;
                }
                else
                {
                    if (!moveLogic.CanMoveToSpecial(specialAbilityToUse.TargetDirections))
                    {
                        chosenActionType = BattleActionType.Move;
                        currentDecision = AIDecision.MoveToUseSpecial;
                        Decide();
                        return;
                    }
                }
            }

            if (ShouldGetAway())
            {
                chosenActionType = BattleActionType.Move;
                currentDecision = AIDecision.Escape;
                Decide();
                return;
            }

            if (attackLogic.IsPlayerInAttackRange())
            {
                chosenActionType = BattleActionType.Attack;
                currentDecision = AIDecision.Attack;
                Decide();
                return;
            }
            else
            {
                chosenActionType = BattleActionType.Move;
                currentDecision = AIDecision.MoveToUseAttack;
                Decide();
                return;
            }

        }

        private void Decide()
        {
            if (chosenActionType != currentActionType)
            {
                currentActionType = chosenActionType;
                StartCoroutine(MakeDecision());
            }
        }

        private IEnumerator MakeDecision()
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            BattleGridManager.Instance.UpdatePreview();
        }

        private bool ShouldGetAway()
        {
            float healthPercentage = BattleSectionManager.Instance.InTurn.Stats.Health / BattleSectionManager.Instance.InTurn.Stats.BaseHealth;
            int turnsLeft = BattleSectionManager.Instance.TemposRemaining;

            return healthPercentage <= 0.3f && turnsLeft <= BattleSectionManager.Instance.TemposPerPlayer / 4;
        }

        private bool PassesCheck(float valueToCheck)
        {
            return Random.Range(0f, 1f) >= valueToCheck;
        }
    }
}
