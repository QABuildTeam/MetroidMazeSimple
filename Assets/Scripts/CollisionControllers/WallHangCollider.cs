using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class WallHangCollider : ObstacleCollider
    {
        public bool CanHang => HasCollision;
        protected override void SetDistance(Collider other)
        {
            Vector3 center = thisCollider.bounds.center;
            Vector3 closest = other.ClosestPointOnBounds(thisCollider.bounds.center);
            //Vector3 boundsPoint = center + thisCollider.bounds.size / 2 * (closest.x > center.x ? 1 : -1);
            Distance = center - closest;
        }
    }
}
