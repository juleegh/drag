using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class GridLimit : MonoBehaviour
    {
        [SerializeField] private GameObject top;
        [SerializeField] private GameObject bottom;
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;

        public void SetLimit(ActionInput direction, bool isLimit)
        {
            switch (direction)
            {
                case ActionInput.Up:
                    top.SetActive(isLimit);
                    break;
                case ActionInput.Down:
                    bottom.SetActive(isLimit);
                    break;
                case ActionInput.Left:
                    left.SetActive(isLimit);
                    break;
                case ActionInput.Right:
                    right.SetActive(isLimit);
                    break;
            }
        }
    }
}
