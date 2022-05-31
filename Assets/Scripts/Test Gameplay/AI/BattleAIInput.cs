using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleAIInput : BattleActionExecuter
    {
        protected static BattleAIInput instance;
        public static BattleAIInput Instance { get { return instance; } }

        private AIAttackLogic attackLogic;
        private AIDefenseLogic defenseLogic;
        private AIMoveLogic moveLogic;
        private AISpecialLogic specialLogic;

        public AIAttackLogic AttackLogic { get { return attackLogic; } }
        public AIDefenseLogic DefenseLogic { get { return defenseLogic; } }
        public AIMoveLogic MoveLogic { get { return moveLogic; } }
        public AISpecialLogic SpecialLogic { get { return specialLogic; } }

        private float MinDecisionTime { get { return BattleActionTempo.Instance.Frequency * 0.35f; } }
        private float MaxDecisionTime { get { return BattleActionTempo.Instance.Frequency * 0.85f; } }

        [SerializeField] private AIBehaviourTree behaviourTree;

        void Awake()
        {
            instance = this;
            attackLogic = new AIAttackLogic();
            moveLogic = new AIMoveLogic();
            defenseLogic = new AIDefenseLogic();
            specialLogic = new AISpecialLogic(this);
        }

        void Start()
        {
            moveLogic.Initialize();
        }

        public void NewTempo()
        {
            if (BattleSectionManager.Instance.IsPlayerTurn)
            {
                if (BattleSectionManager.Instance.Opponent.Stats.Health <= 0)
                    return;

                if (BattleSectionManager.Instance.TemposRemaining == 1)
                    StartCoroutine(ChooseNextAction());
            }
            else
            {
                behaviourTree.ExecuteNextAction();
                StartCoroutine(ChooseNextAction());
            }
        }

        private IEnumerator ChooseNextAction()
        {
            yield return new WaitForSeconds(Random.Range(MinDecisionTime, MaxDecisionTime));
            behaviourTree.ChooseActionForTurn();
            PickNextAbilityType();
        }

        private void PickNextAbilityType()
        {
            BattleActionType chosenActionType = behaviourTree.GetNextActionType();
            if (chosenActionType != currentActionType)
            {
                currentActionType = chosenActionType;
                BattleGridManager.Instance.UpdatePreview();
            }
        }
    }
}
