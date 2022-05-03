using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TestGameplay
{
    public class BattleSectionUI : MonoBehaviour
    {
        [SerializeField] private Vector3 distance;
        [SerializeField] private TextMeshPro counter;
        int total = 0;
        /*
        [SerializeField] private GameObject markerPrefab;
        [SerializeField] private Transform markerContainer;

        private List<TempoMarkerUI> markers;
    */
        public void Initialize(int tempos)
        {
            /*
                markers = new List<TempoMarkerUI>();

                for (int i = 0; i < tempos; i++)
                {
                    TempoMarkerUI marker = Instantiate(markerPrefab).GetComponent<TempoMarkerUI>();
                    marker.MarkAsCompleted();
                    marker.transform.SetParent(markerContainer);
                    markers.Add(marker);
                }
        */
            counter.text = "";
            total = tempos;
        }

        public void MarkCompleted(int index)
        {
            /*
            markers[markers.Count - index - 1].MarkAsEmpty();
        */
            counter.text = (total - index).ToString();
        }

        public void Clean()
        {
            /*
            foreach (TempoMarkerUI marker in markers)
            {
                marker.MarkAsCompleted();
            }
        */
            counter.text = "";
        }

        public void ToggleOwner(BattleCharacter owner)
        {
            transform.SetParent(owner.transform);
            transform.localPosition = distance;
        }
    }
}
