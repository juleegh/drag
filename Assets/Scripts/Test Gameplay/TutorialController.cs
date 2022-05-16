using System;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using TMPro;

namespace TestGameplay
{
    public class TutorialController : MonoBehaviour
    {
        private static TutorialController instance;
        public static TutorialController Instance { get { return instance; } }

        [SerializeField] private List<TutorialStep> instructions;
        [SerializeField] private bool startsWithTutorial;
        //[SerializeField] private TutorialBillboard billboard;

        private int currentStep = 0;
        public bool IsInTutorial { get { return startsWithTutorial && currentStep < instructions.Count; } }

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            if (startsWithTutorial && instructions.Count > 0)
                LoadCurrentStep();
        }

        public void NextStep()
        {
            currentStep++;
            if (currentStep >= instructions.Count)
            {
                //billboard.ToggleVisible(false);
                BattleRespawn.Instance.SetCheckpoint(BattleSectionManager.Instance.Player, BattleSectionManager.Instance.Player.CurrentPosition);
            }
            else
                LoadCurrentStep();
        }

        private void LoadCurrentStep()
        {
            //billboard.ToggleVisible(true);
            instructions[currentStep].LoadStep();
            //billboard.SetStepText(instructions[currentStep].Instruction);
        }

        private void FinishTutorial()
        {
            UIBattleActionSelection.Instance.ToggleActionVisibility(ActionInput.Up, true);
            UIBattleActionSelection.Instance.ToggleActionVisibility(ActionInput.Down, true);
            UIBattleActionSelection.Instance.ToggleActionVisibility(ActionInput.Left, true);
            UIBattleActionSelection.Instance.ToggleActionVisibility(ActionInput.Right, true);
        }
    }

    /*
        public class TutorialBillboard : MonoBehaviour
        {
            [SerializeField] private GameObject container;
            [SerializeField] private TextMeshProUGUI text;

            public void ToggleVisible(bool visible)
            {
                container.SetActive(visible);
            }

            public void SetStepText(string content)
            {
                text.text = content;
            }
        }
    */
}
