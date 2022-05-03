using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{

    public class BattleDefenseIndicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer up;
        [SerializeField] private SpriteRenderer down;
        [SerializeField] private SpriteRenderer left;
        [SerializeField] private SpriteRenderer right;

        [SerializeField] private Sprite oneShield;
        [SerializeField] private Sprite twoShields;
        [SerializeField] private Sprite threeShields;


        public void UpdateDefense(Dictionary<ActionInput, int> newDefense)
        {
            up.sprite = GetShieldSprite(newDefense[ActionInput.Up]);
            down.sprite = GetShieldSprite(newDefense[ActionInput.Down]);
            left.sprite = GetShieldSprite(newDefense[ActionInput.Left]);
            right.sprite = GetShieldSprite(newDefense[ActionInput.Right]);
        }

        private Sprite GetShieldSprite(int quantity)
        {
            if (quantity == 3)
                return threeShields;
            if (quantity == 2)
                return twoShields;
            if (quantity == 1)
                return oneShield;

            return null;
        }
    }
}
