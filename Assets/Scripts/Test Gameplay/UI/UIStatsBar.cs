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
        [SerializeField] private TextMeshProUGUI tens;

        private BattleIconUI[] icons;
        private int displayedIcons = 10;
        private int currentValue;

        void Awake()
        {
            icons = new BattleIconUI[displayedIcons];
            for (int i = 0; i < displayedIcons; i++)
            {
                BattleIconUI newIcon = Instantiate(prefab).GetComponent<BattleIconUI>();
                newIcon.transform.SetParent(container);
                newIcon.transform.localPosition = Vector3.zero;
                icons[i] = newIcon;
            }

            StartCoroutine(Refresh());
        }

        private IEnumerator Refresh()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < displayedIcons; i++)
            {
                icons[i].gameObject.SetActive(false);
                icons[i].gameObject.SetActive(true);
            }
        }

        public void Fill(int fill)
        {
            if (icons == null || fill == currentValue)
                return;

            /*
                        int aboveTen = (fill / displayedIcons);
                        int belowTen = fill - (aboveTen * displayedIcons);
                        tens.text = "x" + aboveTen;
                        tens.gameObject.SetActive(aboveTen > 0);
            */
            tens.gameObject.SetActive(false);
            bool positiveChange = currentValue < fill;

            for (int i = 0; i < displayedIcons; i++)
            {
                //icons[i].Toggle(i + 1 <= belowTen, positiveChange);
                icons[i].Toggle(i + 1 <= fill, positiveChange);
            }

            currentValue = fill;
        }
    }
}
