using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace TestGameplay
{
    public class UIStatsBar : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;

        private BattleIconUI[] icons;
        private int currentValue;

        public void Setup(int displayedIcons)
        {
            if (icons == null)
            {
                icons = new BattleIconUI[displayedIcons];
                for (int i = 0; i < displayedIcons; i++)
                {
                    BattleIconUI newIcon = Instantiate(prefab).GetComponent<BattleIconUI>();
                    newIcon.transform.SetParent(container);
                    newIcon.transform.localPosition = Vector3.right * i / 2;
                    newIcon.transform.localEulerAngles = Vector3.zero;
                    newIcon.transform.localScale = Vector3.one;
                    newIcon.Initialize(true);
                    icons[i] = newIcon;
                    if (gameObject.activeInHierarchy)
                        StartCoroutine(Refresh());
                }
            }
            else
            {
                for (int i = 0; i < displayedIcons; i++)
                {
                    icons[i].Initialize(true);
                }
            }

        }

        private IEnumerator Refresh()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].gameObject.SetActive(false);
                icons[i].gameObject.SetActive(true);
            }
        }

        public void Fill(int fill)
        {
            if (icons == null || fill == currentValue)
                return;

            bool positiveChange = currentValue < fill;

            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].Toggle(i + 1 <= fill, positiveChange);
            }

            currentValue = fill;
        }
    }
}
