using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PerformingChoreoController : MonoBehaviour, RequiredComponent
{
    private Choreography choreography;
    public Choreography Choreography { get { return choreography; } }

    private static PerformingChoreoController instance;
    public static PerformingChoreoController Instance { get { return instance; } }
    private KeyValuePair<int, DanceMove[]> currentTempo;
    private int currentTempoIndex;
    private int characterStamina;
    public int CharacterStamina { get { return characterStamina; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, LoadLevelChoreography);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovesShifted, CheckIndex);
    }

    private void LoadLevelChoreography()
    {
        choreography = new Choreography();
        choreography.LoadChoreo(ProgressManager.Instance.BossLevel);

        currentTempoIndex = 0;
        currentTempo = choreography.MovesPerTime.ToList()[currentTempoIndex];
        characterStamina = ProgressManager.Instance.Stamina;
    }

    private void CheckIndex()
    {
        if (PerformSystem.Instance.CurrentMoveIndex > currentTempo.Key + 1)
        {
            currentTempoIndex++;
            if (currentTempoIndex < choreography.MovesPerTime.Count)
                currentTempo = choreography.MovesPerTime.ToList()[currentTempoIndex];
            PerformingEventsManager.Instance.Notify(PerformingEvent.ShiftedTempo);
        }
    }

    public DanceMove GetMoveFromType(MoveType moveType)
    {
        return choreography.GetMoveInTempoByType(PerformSystem.Instance.CurrentMoveIndex, moveType);
    }

    public DanceMove[] GetOptionsForTempo()
    {
        return currentTempo.Value;
    }

    public void PlayerPerformedMove(DanceMove danceMove)
    {
        characterStamina -= danceMove.StaminaRequired;
    }
}