using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CrawlCollider : ObstacleCollider
    {
        public bool CanCrawl => HasCollision;
    }
}
