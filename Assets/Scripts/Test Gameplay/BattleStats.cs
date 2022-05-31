using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleStats
    {
        private int baseStamina;
        private int stamina;
        public int Stamina { get { return stamina; } }

        private int health;
        private int baseHealth;
        public int Health { get { return health; } }
        public int BaseHealth { get { return baseHealth; } }

        private Dictionary<ActionInput, int> defense;
        public Dictionary<ActionInput, int> Defense { get { return defense; } }

        private int attack;
        public int Attack { get { return attack; } }

        public BattleStats(int baseHealth, int baseStamina)
        {
            this.baseHealth = baseHealth;
            health = baseHealth;
            this.baseStamina = baseStamina;
            stamina = baseStamina;

            defense = new Dictionary<ActionInput, int>();
            defense[ActionInput.Up] = 0;
            defense[ActionInput.Down] = 0;
            defense[ActionInput.Left] = 0;
            defense[ActionInput.Right] = 0;
        }

        public void ReceiveDamage(Vector2Int direction, int damage)
        {
            bool takesDamage = true;

            if (direction.x < 0)
            {
                if (defense[ActionInput.Left] > 0)
                {
                    //defense[ActionInput.Left]--;
                    takesDamage = false;
                }
            }
            else if (direction.x > 0)
            {
                if (defense[ActionInput.Right] > 0)
                {
                    //defense[ActionInput.Right]--;
                    takesDamage = false;
                }
            }

            if (direction.y < 0)
            {
                if (defense[ActionInput.Down] > 0)
                {
                    //defense[ActionInput.Down]--;
                    takesDamage = false;
                }
            }
            else if (direction.y > 0)
            {
                if (defense[ActionInput.Up] > 0)
                {
                    //defense[ActionInput.Up]--;
                    takesDamage = false;
                }
            }

            if (!takesDamage)
                return;

            health -= damage;
        }

        public void IncreaseHealth(int heal)
        {
            health += heal;
        }

        public void DecreaseStamina(int tired)
        {
            stamina -= tired;
        }

        public void BoostDefense(ActionInput direction, int boost)
        {
            defense[direction] += boost;
        }

        public void ResetStamina()
        { 
            stamina = baseStamina;
        }

        public void ResetBoosts()
        {
            defense[ActionInput.Up] = 0;
            defense[ActionInput.Down] = 0;
            defense[ActionInput.Left] = 0;
            defense[ActionInput.Right] = 0;
        }
    }
}
