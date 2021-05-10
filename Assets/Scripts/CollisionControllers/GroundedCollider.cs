using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class GroundedCollider : ObstacleCollider
    {
        public bool IsGrounded => HasCollision;
    }
}
