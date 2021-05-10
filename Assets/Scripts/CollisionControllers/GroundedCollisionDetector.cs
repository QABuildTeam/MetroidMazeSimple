using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class GroundedCollisionDetector : RaycastCollisionDetector
    {
        public bool IsGrounded => HasCollision;
    }
}
