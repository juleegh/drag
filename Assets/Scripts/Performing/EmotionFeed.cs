using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionFeed
{
    private Dictionary<MoveType, float> emotionsFeed;
    public Dictionary<MoveType, float> EmotionsFeed { get { return emotionsFeed; } }

    public EmotionFeed()
    {
        emotionsFeed = new Dictionary<MoveType, float>();
    }

    public void GenerateRandom()
    {
        emotionsFeed[MoveType.AType] = Random.Range(0f, 1f);
        emotionsFeed[MoveType.BType] = Random.Range(0f, 1f);
        emotionsFeed[MoveType.XType] = Random.Range(0f, 1f);
        emotionsFeed[MoveType.YType] = Random.Range(0f, 1f);
        PerformingEventsManager.Instance.Notify(PerformingEvent.CreatedAudienceEmotions);
        Clean();
    }

    private void Clean()
    {
        emotionsFeed[MoveType.AType] = 0f;
        emotionsFeed[MoveType.BType] = 0f;
        emotionsFeed[MoveType.XType] = 0f;
        emotionsFeed[MoveType.YType] = 0f;
    }

    public void ReactToMove(Move move)
    {
        if (emotionsFeed.ContainsKey(move.moveType))
            emotionsFeed[move.moveType] += move.score;
        else
            emotionsFeed[move.moveType] = move.score;
    }
}
