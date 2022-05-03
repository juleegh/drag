using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class ActionsLeftOrganizer : MonoBehaviour
    {
        [SerializeField] private int rows;
        [SerializeField] private int columns;

        [SerializeField] private float distance;

        private void Update()
        {
            TempoMarkerUI[] TempoMarkerUIs = GetComponentsInChildren<TempoMarkerUI>();

            int current = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (current >= TempoMarkerUIs.Length)
                        return;

                    TempoMarkerUI TempoMarkerUI = TempoMarkerUIs[current];
                    TempoMarkerUI.transform.localPosition = Vector3.left * column * distance + Vector3.forward * row * distance;
                    current++;
                }
            }
        }
    }
}
