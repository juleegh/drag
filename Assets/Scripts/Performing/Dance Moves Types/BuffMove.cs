using UnityEngine;

public class BuffMove : DanceMove
{
    [SerializeField] private DefenseBuff defenseBuff;
    public DefenseBuff DefenseBuff { get { return defenseBuff; } }
}