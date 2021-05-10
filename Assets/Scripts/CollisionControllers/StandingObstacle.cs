using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class StandingObstacle : ObstacleCollider
    {
        public bool CanStandUp => !HasCollision;
    }
}
