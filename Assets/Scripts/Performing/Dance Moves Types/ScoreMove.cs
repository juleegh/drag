using UnityEngine;

public class ScoreMove : DanceMove
{
    [SerializeField] private int score;
    public int Score { get { return score; } }
}