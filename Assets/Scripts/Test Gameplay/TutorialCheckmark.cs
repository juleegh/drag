using System;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using TMPro;

namespace TestGameplay
{
    public class TutorialCheckmark : MonoBehaviour
    {
        [SerializeField] private BoxCollider playerDetector;

        private void OnTriggerEnter(Collider other)
        {
            BattleCharacter player = other.GetComponent<BattleCharacter>();
            if (player != null)
            {
                TutorialController.Instance.NextStep();
                Destroy(gameObject);
            }
        }
    }
}