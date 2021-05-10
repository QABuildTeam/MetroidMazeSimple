using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Linq;

namespace PillarRolling
{
    public class StairsBuilder : MonoBehaviour
    {
        [SerializeField]
        private int pillarId = 1;
        [SerializeField]
        private PillarParameters pillarParameters;
        [SerializeField]
        private GameObject stepPrefab;
        [SerializeField]
        private GameObject verticalObstaclePrefab;

        PillarParameters.PillarDescriptor pillar;

        private void Start()
        {
            pillar = pillarParameters.pillars.FirstOrDefault(p => p.pillarId == pillarId);
            if (pillar != null)
            {
                BuildStairs();
            }
        }

        public void BuildStairs()
        {
            foreach (var stairDescriptor in pillar.stairs)
            {
                float angle = stairDescriptor.startAngle;
                float obstacleHeight = 0;
                bool obstaclePlaced = false;
                float stairsHeight = Mathf.Abs(stairDescriptor.fullStairsRotations / stairDescriptor.stepAngle * stairDescriptor.stepHeight);
                for (float y = stairDescriptor.startY; y < stairDescriptor.startY + stairsHeight; y += stairDescriptor.stepHeight, angle += stairDescriptor.stepAngle, obstacleHeight += stairDescriptor.stepHeight)
                {
                    GameObject o = Instantiate(stepPrefab, transform, true);
                    var position = o.transform.position;
                    position.y = y;
                    o.transform.position = position;
                    o.transform.Rotate(Vector3.up, angle);
                    if (stairDescriptor.setObstacleAtHeight && obstacleHeight >= stairDescriptor.obstacleAtHeight && !obstaclePlaced)
                    {
                        GameObject obstacle = Instantiate(verticalObstaclePrefab, transform, true);
                        position = obstacle.transform.position;
                        position.y = (y + stairDescriptor.startY) / 2;
                        obstacle.transform.position = position;
                        obstacle.transform.Rotate(Vector3.up, angle);
                        obstaclePlaced = true;
                    }
                }
            }
        }
    }
}
