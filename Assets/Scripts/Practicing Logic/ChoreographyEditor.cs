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

    public void SaveMoveToTempo(int tempoIndex, int position, DanceMove danceMove)
    {
        int tempo = Song.SongBuffs.ToList()[tempoIndex].Key;
        choreography.AddMoveToTempo(tempo, position, danceMove);
        PosePerformer.Instance.HitPose(PoseType.Idle);
        CancelPreview();
        PracticeEventsManager.Instance.Notify(PracticeEvents.ChoreographyUpdated);
        choreography.SaveChoreo();
    }

    public int GetTentativeStamina(int tempoIndex, int position, DanceMove danceMove)
    {
        int tempo = Song.SongBuffs.ToList()[tempoIndex].Key;
        int stamina = choreography.GetTotalStamina();
        stamina -= choreography.MovesPerTime[tempo][position].StaminaRequired;
        stamina += danceMove.StaminaRequired;
        return stamina;
    }

    public void PreviewMove(DanceMove danceMove)
    {
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
