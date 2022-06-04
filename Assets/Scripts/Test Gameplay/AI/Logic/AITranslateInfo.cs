using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AITranslateInfo
    {
        public Vector2Int distance;
        public Vector2Int finalPos;
        public List<Vector2Int> path;
        public int steps;

        public AITranslateInfo()
        {
            path = new List<Vector2Int>();
        }
    }
}
