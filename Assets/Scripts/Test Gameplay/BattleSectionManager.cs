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
        private int sectionTempoCount;

        void Awake()
        {
            instance = this;
            sectionTempoCount = 0;
            currentTurn = player;
            notInTurn = opponent;
            sectionUI.Initialize(temposPerPlayer);
        }

        public void NewTempo()
        {
            sectionUI.MarkCompleted(sectionTempoCount);
        }

        public void FinishedTempo()
        {
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
        }
    }
}
