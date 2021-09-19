using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoCounter : MonoBehaviour, RequiredComponent
{
    public static TempoCounter Instance { get { return instance; } }
    private static TempoCounter instance;

    private float frequency;
    private bool preBeatFrame;
    private bool postBeatFrame;
    bool firstTime;

    WaitForSeconds unaceptable;
    WaitForSeconds preAcceptable;
    WaitForSeconds postAcceptable;

    public bool IsOnPreTempo { get { return preBeatFrame; } }
    public bool IsOnPostTempo { get { return postBeatFrame; } }

    [SerializeField] private float delayDuration;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovePerformed, ClearDelay);
    }

    public void SetTempo(float freq)
    {
        frequency = freq;
    }

    public void StartTempoCount()
    {
        firstTime = true;
        unaceptable = new WaitForSeconds(frequency * (1 - PerformSystem.Instance.MovesProperties.AcceptablePercentage));
        preAcceptable = new WaitForSeconds(frequency * PerformSystem.Instance.MovesProperties.AcceptablePercentage * 0.65f);
        postAcceptable = new WaitForSeconds(frequency * PerformSystem.Instance.MovesProperties.AcceptablePercentage * 0.35f);
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
        if (SongSequence.Instance.Slots[PerformSystem.Instance.CurrentMoveIndex].buff != MoveBuff.None)
        {
            StartCoroutine(PlayCooldown());
            PerformingEventsManager.Instance.Notify(PerformingEvent.CurrentTempoStarted);
        }
        else
            StartCoroutine(PostTempo());
    }

    private IEnumerator PlayCooldown()
    {
        yield return new WaitForSeconds(delayDuration);
        StartCoroutine(PostTempo());
    }

    private void ClearDelay()
    {
        StopAllCoroutines();
        StartCoroutine(PostTempo());
    }

    private IEnumerator PostTempo()
    {
        yield return preAcceptable;
        PerformingEventsManager.Instance.Notify(PerformingEvent.TempoEnded);
        preBeatFrame = false;
        postBeatFrame = true;
        yield return postAcceptable;
        postBeatFrame = false;
        StartCoroutine(PreTempo());
    }
}
