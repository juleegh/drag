using System;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using TMPro;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "Tutorial Step")]
    public class TutorialStep : ScriptableObject
    {
        [Serializable]
        public class InputVisibility : SerializableDictionaryBase<ActionInput, bool> { }

        [SerializeField] private InputVisibility inputVisibility;
        //[SerializeField] private string instruction;
        //public string Instruction { get { return instruction; } }

        public void LoadStep()
        {
            foreach (KeyValuePair<ActionInput, bool> input in inputVisibility)
            {
                UIBattleActionSelection.Instance.ToggleActionVisibility(input.Key, input.Value);
            }
        }
    }
}