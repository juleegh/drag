using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlot
{
    public Move move;
    //move { get { return theMove; } set { theMove = value; Debug.LogError(value); } }
    public Move theMove;
    public MoveBuff buff;
    public bool performed;
    public bool correct;

    public void SelectRandomBuff()
    {
        List<MoveBuff> buffs = new List<MoveBuff>();
        buffs.Add(MoveBuff.None);
        buffs.Add(MoveBuff.Half);
        buffs.Add(MoveBuff.Double);
        buff = buffs[Random.Range(0, buffs.Count)];
        performed = false;
        correct = false;
    }

    public float GetMultiplier()
    {
        switch (buff)
        {
            case MoveBuff.None:
                return 1f;
            case MoveBuff.Half:
                return 0.5f;
            case MoveBuff.Double:
                return 2.0f;
        }

        return 1f;
    }
}
