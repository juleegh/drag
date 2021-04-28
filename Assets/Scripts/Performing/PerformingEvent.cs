using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerformingEvent
{
    DependenciesLoaded,
    WaitingForSequenceCreation,
    SlotAdded,
    SequenceCreated,
    CreatedAudienceEmotions,
    WaitingForSequenceInput,
    PlayerSelectedMove,
    SequenceInputComplete,
    MovePerformed,
    TempoEnded,
    MovesShifted
}
