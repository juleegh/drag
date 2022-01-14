using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleActionTempo : MonoBehaviour
    {
        public static BattleActionTempo Instance { get { return instance; } }
        private static BattleActionTempo instance;

        [SerializeField] private float frequency;
        [SerializeField] private BattleUIElement indicator;
        [SerializeField] private float acceptablePercentage;
        private bool preBeatFrame;
        private bool postBeatFrame;
        bool firstTime;

        WaitForSeconds unaceptable;
        WaitForSeconds preAcceptable;
        WaitForSeconds postAcceptable;

        public bool IsOnPreTempo { get { return preBeatFrame; } }
        public bool IsOnPostTempo { get { return postBeatFrame; } }

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            StartTempoCount();
        }

        public void StartTempoCount()
        {
            firstTime = true;
            unaceptable = new WaitForSeconds(frequency * (1 - acceptablePercentage));
            preAcceptable = new WaitForSeconds(frequency * acceptablePercentage * 0.65f);
            postAcceptable = new WaitForSeconds(frequency * acceptablePercentage * 0.35f);
            StartCoroutine(PreTempo());
        }

        public void StopTempoCount()
        {
            StopAllCoroutines();
        }

        private IEnumerator PreTempo()
        {
            if (firstTime)
            {
                yield return postAcceptable;
                firstTime = false;
            }
            yield return unaceptable;
            preBeatFrame = true;
            StartCoroutine(PostTempo());
        }

        private IEnumerator PostTempo()
        {
            yield return preAcceptable;
            indicator.Twinkle();
            //PerformingEventsManager.Instance.Notify(PerformingEvent.TempoEnded);
            BattleSectionManager.Instance.NewTempo();
            preBeatFrame = false;
            postBeatFrame = true;
            yield return postAcceptable;
            postBeatFrame = false;
            BattleSectionManager.Instance.FinishedTempo();
            StartCoroutine(PreTempo());
        }
    }

}
