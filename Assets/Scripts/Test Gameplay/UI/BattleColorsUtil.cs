using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleColorsUtil : MonoBehaviour
    {
        private static BattleColorsUtil instance;
        public static BattleColorsUtil Instance { get { return instance; } }

        [SerializeField] private Color move;
        [SerializeField] private Color attack;
        [SerializeField] private Color defend;
        [SerializeField] private Color stamina;
        [SerializeField] private Color health;

        public Color Move { get { return move; } }
        public Color Attack { get { return attack; } }
        public Color Defend { get { return defend; } }
        public Color Stamina { get { return stamina; } }
        public Color Health { get { return health; } }

        void Awake()
        {
            instance = this;
        }
    }
}
