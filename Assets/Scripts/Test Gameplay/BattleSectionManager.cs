using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleSectionManager : MonoBehaviour
    {
        private static BattleSectionManager instance;
        public static BattleSectionManager Instance { get { return instance; } }

        [SerializeField] private int temposPerPlayer;

        private BattleCharacter currentTurn;
        private BattleCharacter notInTurn;
        [SerializeField] private BattleCharacter player;
        [SerializeField] private BattleCharacter opponent;
        [SerializeField] private BattleSectionUI sectionUI;
        public BattleCharacter Player { get { return player; } }
        public BattleCharacter Opponent { get { return opponent; } }
        public BattleCharacter InTurn { get { return currentTurn; } }
        public BattleCharacter NotInTurn { get { return notInTurn; } }
        public bool IsPlayerTurn { get { return currentTurn == player; } }
        public BattleActionExecuter CurrentExecuter { get { return IsPlayerTurn ? (BattleActionExecuter)PlayerActionsManager.Instance : (BattleActionExecuter)BattleAIInput.Instance; } }
        private int sectionTempoCount;
        public int TemposRemaining { get { return temposPerPlayer - sectionTempoCount; } }
        public int TemposPerPlayer { get { return temposPerPlayer; } }

        void Awake()
        {
            instance = this;
            sectionTempoCount = 0;
            sectionUI.Initialize(temposPerPlayer);
            currentTurn = player;
            notInTurn = opponent;
            BattleActionTempo.Instance.StartTempoCount();
        }

        public void ResetTurns()
        {
            sectionTempoCount = 0;
            currentTurn = player;
            notInTurn = opponent;
            BattleActionTempo.Instance.StopTempoCount();
            BattleActionTempo.Instance.StartTempoCount();
        }

        public void NewTempo()
        {
            if (TutorialController.Instance.IsInTutorial)
                return;
            sectionUI.MarkCompleted(sectionTempoCount);
        }

        public void FinishedTempo()
        {
            if (TutorialController.Instance.IsInTutorial)
                return;

            sectionTempoCount++;
            if (sectionTempoCount == temposPerPlayer)
            {
                sectionTempoCount = 0;
                sectionUI.Clean();
                ToggleTurn();
            }
        }

        private void ToggleTurn()
        {
            currentTurn = currentTurn == player ? opponent : player;
            notInTurn = notInTurn == player ? opponent : player;
            currentTurn.ResetStats();
            sectionUI.ToggleOwner(currentTurn);
            BattleGridManager.Instance.UpdatePreview();
        }
    }
}
