using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleSectionUI : MonoBehaviour
    {
        [SerializeField] private GameObject markerPrefab;
        [SerializeField] private Transform markerContainer;

        private List<TempoMarkerUI> markers;

        public void Initialize(int tempos)
        {
            markers = new List<TempoMarkerUI>();

            for (int i = 0; i < tempos; i++)
            {
                TempoMarkerUI marker = Instantiate(markerPrefab).GetComponent<TempoMarkerUI>();
                marker.MarkAsEmpty();
                marker.transform.SetParent(markerContainer);
                markers.Add(marker);
            }
        }

        public void MarkCompleted(int index)
        {
            markers[index].MarkAsCompleted();
        }

        public void Clean()
        {
            foreach (TempoMarkerUI marker in markers)
            {
                marker.MarkAsEmpty();
            }
        }
    }
}
