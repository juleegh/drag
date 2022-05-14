using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;


namespace TestGameplay
{
    public class UIBattleActionSelection : MonoBehaviour
    {
        private static UIBattleActionSelection instance;
        public static UIBattleActionSelection Instance { get { return instance; } }

        [Serializable]
        public class ActionsUIDictionary : SerializableDictionaryBase<ActionInput, BattleUIElement> { }

        [SerializeField] private ActionsUIDictionary actionTypes;

        void Awake()
        {
            instance = this;
        }

        public void PaintSelectedAction(ActionInput actionInput)
        {
            foreach (KeyValuePair<ActionInput, BattleUIElement> image in actionTypes)
            {
                actionTypes[image.Key].Hightlight(actionInput == image.Key);
            }
        }

        public void ToggleActionVisibility(ActionInput actionInput, bool visible)
        {
            actionTypes[actionInput].ToggleVisible(visible);
        }
    }
}
