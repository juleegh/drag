using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestGameplay
{
    public class TempoMarkerUI : MonoBehaviour
    {
        [SerializeField] private Image marker;
        [SerializeField] private Color empty;
        [SerializeField] private Color filled;

        //float colorDelay = 0.3f;

        public void MarkAsEmpty()
        {
            marker.color = empty;
        }

        public void MarkAsCompleted()
        {
            marker.color = filled;
        }
    }
}
