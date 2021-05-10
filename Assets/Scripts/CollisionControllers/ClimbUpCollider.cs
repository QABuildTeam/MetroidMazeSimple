using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class ClimbUpCollider : ObstacleCollider
    {
        public bool CanClimbUp => !HasCollision;
    }
}
