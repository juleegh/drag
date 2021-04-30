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
        Clean();
    }

    public void DefineTargets(float totalEmotions)
    {
        float remaining = totalEmotions;
        float random = Random.Range(0f, remaining);
        targetEmotions[MoveType.AType] += random;

        remaining -= random;
        random = Random.Range(0f, remaining);
        targetEmotions[MoveType.BType] += random;

        remaining -= random;
        random = Random.Range(0f, remaining);
        targetEmotions[MoveType.XType] += random;

        remaining -= random;
        targetEmotions[MoveType.YType] += remaining;
        PerformingEventsManager.Instance.Notify(PerformingEvent.CreatedAudienceEmotions);
    }

    private void Clean()
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

    public void ReactToMove(Move move)
    {
        if (currentEmotions.ContainsKey(move.moveType))
            currentEmotions[move.moveType] += move.score;
        else
            currentEmotions[move.moveType] = move.score;
    }
}
