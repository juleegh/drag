using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace TestGameplay
{
    public class TurnChangeUI : MonoBehaviour
    {
        private static TurnChangeUI instance;
        public static TurnChangeUI Instance { get { return instance; } }

        [SerializeField] private Image background;
        [SerializeField] private Image banner;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Color playerColor;
        [SerializeField] private Color opponentColor;
        private Color empty = new Color(0, 0, 0, 0);
        private float delay = 0.3f;
        private Vector3 normalScale;
        private Vector3 tempoScale;

        private void Awake()
        {
            instance = this;
            normalScale = title.transform.localScale;    
            tempoScale = normalScale * 1.1f;
            title.gameObject.SetActive(true);
        }

        private void Start()
        {
            Color current = BattleSectionManager.Instance.IsPlayerTurn ? playerColor : opponentColor;
            banner.color = current;
            title.text = BattleSectionManager.Instance.IsPlayerTurn ? "Player Turn" : "Opponent Turn";
        }

        public void ShowTurnChange()
        {
            background.color = empty;
            Color current = BattleSectionManager.Instance.IsPlayerTurn ? playerColor : opponentColor;
            background.DOColor(current, delay).OnComplete(() => { background.DOColor(empty, delay); } );
            banner.DOColor(empty, delay).OnComplete(() => { banner.DOColor(current, delay); } );

            title.text = BattleSectionManager.Instance.IsPlayerTurn ? "Player Turn" : "Opponent Turn";
            title.transform.DOScale(tempoScale, delay).OnComplete(() => { title.transform.DOScale(normalScale, delay); } );
        }
    }
}
