using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

namespace TestGameplay
{
    public interface IGridDanger
    {
        public Vector2Int GetCurrentPosition();
        public Vector2Int GetDelta();
        public Vector2Int GetAffectedArea();
        public int GetDamage();
    }

}

