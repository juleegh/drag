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
        [SerializeField] private float acceptablePercentage;
        [SerializeField] private AudioSource audioSource;
        private bool preBeatFrame;
        private bool postBeatFrame;
        bool firstTime;
        private float preFrequenceTime;

        WaitForSeconds unaceptable;
        WaitForSeconds preAcceptable;
        WaitForSeconds postAcceptable;

        public bool IsOnPreTempo { get { return preBeatFrame; } }
        public bool IsOnPostTempo { get { return postBeatFrame; } }
        private CellTempoCounter[] counters;

        void Awake()
        {
            instance = this;
            counters = FindObjectsOfType<CellTempoCounter>();
            preFrequenceTime = frequency * acceptablePercentage * 0.65f / 2;
            foreach (CellTempoCounter counter in counters)
                counter.SetupBlingTime(preFrequenceTime);
        }

        public void StartTempoCount()
        {
            firstTime = true;
            unaceptable = new WaitForSeconds(frequency * (1 - acceptablePercentage));
            preAcceptable = new WaitForSeconds(preFrequenceTime);
            postAcceptable = new WaitForSeconds(frequency * acceptablePercentage * 0.35f);
            StartCoroutine(PreTempo());
        }

        public void StopTempoCount()
        {
            StopAllCoroutines();
            audioSource.Stop();
        }

        private IEnumerator PreTempo()
        {
            if (firstTime)
            {
                yield return postAcceptable;
                firstTime = false;
                audioSource.Play();
            }
            yield return unaceptable;
            preBeatFrame = true;
            StartCoroutine(PostTempo());
        }

        private IEnumerator PostTempo()
        {
            yield return preAcceptable;
            //indicator.Cooldown();
            foreach (CellTempoCounter counter in counters)
                counter.Toggle();
            BattleSectionManager.Instance.NewTempo();
            BattleAIInput.Instance.NewTempo();
            yield return preAcceptable;
            preBeatFrame = false;
            postBeatFrame = true;
            yield return postAcceptable;
            postBeatFrame = false;
            BattleSectionManager.Instance.FinishedTempo();
            StartCoroutine(PreTempo());
        }
    }

}
