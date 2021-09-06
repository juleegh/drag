using UnityEngine;

public class DefenseMove : DanceMove
{
    [SerializeField] private DefenseBuff defenseBuff;
    public DefenseBuff DefenseBuff { get { return defenseBuff; } }
}