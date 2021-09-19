using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "Boss Choreo")]
public class OpponentChoreography : ScriptableObject
{
    [Serializable]
    public class MovesByType : SerializableDictionaryBase<MoveType, DanceMove> { }

    [Serializable]
    public class MovesPerTempo : SerializableDictionaryBase<int, MovesByType> { }

    [SerializeField] private MovesPerTempo choreography;

    public MovesPerTempo Choreography { get { return choreography; } }

    [SerializeField] private Song song;

    [ContextMenu("Load Tempos")]
    private void LoadTempos()
    {
        choreography = new MovesPerTempo();
        foreach (KeyValuePair<int, MoveBuff> moves in song.SongBuffs)
        {
            choreography.Add(moves.Key, new MovesByType());
            for (int i = 0; i < PerformanceConversions.MoveTypesQuantity; i++)
            {
                choreography[moves.Key].Add(PerformanceConversions.ConvertMoveTypeFromIndex(i), null);
            }
        }
    }

    public DanceMove GetResponseFromTempo(int tempo, MoveType moveType)
    {
        return choreography[tempo][moveType];
    }
}
