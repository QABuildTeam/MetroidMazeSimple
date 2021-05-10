using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    [System.Serializable]
    public class StairsDescriptor
    {
        public float stepHeight = 0.15f;
        public float stepAngle = -3f;
        public float startAngle = 0f;
        public float startY = 1f;
        public float fullStairsRotations = 400f;
        public bool setObstacleAtHeight = true;
        public float obstacleAtHeight = 1f;
    }
}
