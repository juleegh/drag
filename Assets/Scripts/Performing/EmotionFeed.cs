using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionFeed
{
    private Dictionary<MoveType, float> targetEmotions;
    private Dictionary<MoveType, float> currentEmotions;
    public Dictionary<MoveType, float> TargetEmotions { get { return targetEmotions; } }
    public Dictionary<MoveType, float> CurrentEmotions { get { return currentEmotions; } }

    public EmotionFeed()
    {
        currentEmotions = new Dictionary<MoveType, float>();
        targetEmotions = new Dictionary<MoveType, float>();
        CleanAll();
    }

    public void DefineTargets(List<MoveSlot> sequenceMoves)
    {
        List<MoveType> types = new List<MoveType>();
        types.Add(MoveType.AType);
        types.Add(MoveType.BType);
        types.Add(MoveType.XType);
        types.Add(MoveType.YType);

        for (int i = 0; i < sequenceMoves.Count; i++)
        {
            //if (sequenceMoves[i].GetMultiplier() != 0)
            //Debug.LogError(sequenceMoves[i].GetMultiplier());
            targetEmotions[types[Random.Range(0, types.Count)]] += sequenceMoves[i].GetMultiplier() * 200f;
        }

        PerformingEventsManager.Instance.Notify(PerformingEvent.CreatedAudienceEmotions);
    }

    private void CleanAll()
    {
        currentEmotions[MoveType.AType] = 0f;
        currentEmotions[MoveType.BType] = 0f;
        currentEmotions[MoveType.XType] = 0f;
        currentEmotions[MoveType.YType] = 0f;

        targetEmotions[MoveType.AType] = 0f;
        targetEmotions[MoveType.BType] = 0f;
        targetEmotions[MoveType.XType] = 0f;
        targetEmotions[MoveType.YType] = 0f;
    }

    public void ReactToMove(PerformedMove move)
    {
        if (currentEmotions.ContainsKey(move.moveType))
            currentEmotions[move.moveType] += move.score;
        else
            currentEmotions[move.moveType] = move.score;
    }
}
