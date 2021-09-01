using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChoreographyEditor : MonoBehaviour, RequiredComponent
{
    private static ChoreographyEditor instance;
    public static ChoreographyEditor Instance { get { return instance; } }

    private Choreography choreography;
    public Choreography Choreography { get { return choreography; } }
    private Song Song { get { return ProgressManager.Instance.CurrentLevel.BattleSong; } }

    private float previewDelay = 0;
    private PoseType previewPose;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.DependenciesLoaded, LoadLevelChoreography);
    }

    private void LoadLevelChoreography()
    {
        choreography = new Choreography();
        choreography.LoadChoreo(ProgressManager.Instance.BossLevel);
        PracticeEventsManager.Instance.Notify(PracticeEvents.ChoreographyLoaded);
    }

    public void SaveMoveToTempo(int tempoIndex, int position, int danceMoveIndex)
    {
        int tempo = Song.SongBuffs.ToList()[tempoIndex].Key;
        DanceMove danceMove = DanceMovesManager.Instance.DanceMovesList.ToList()[danceMoveIndex].Value;
        choreography.AddMoveToTempo(tempo, position, danceMove);
        PosePerformer.Instance.HitPose(PoseType.Idle);
        CancelPreview();
        PracticeEventsManager.Instance.Notify(PracticeEvents.ChoreographyUpdated);
        choreography.SaveChoreo();
    }

    public void PreviewMove(int danceMoveIndex)
    {
        DanceMove danceMove = DanceMovesManager.Instance.DanceMovesList.ToList()[danceMoveIndex].Value;
        previewDelay = 0.8f;
        previewPose = danceMove.PoseType;
    }

    public void CancelPreview()
    {
        previewDelay = -1;
    }

    private void Update()
    {
        if (previewDelay > 0)
        {
            previewDelay -= Time.deltaTime;
            if (previewDelay <= 0)
                PosePerformer.Instance.HitPose(previewPose);
        }
    }

}
