using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Linq;

namespace PillarRolling
{
    public class BoulderBuilder : MonoBehaviour
    {
        [SerializeField]
        private int pillarId = 1;
        [SerializeField]
        private PillarParameters pillarParameters;
        [SerializeField]
        private GameObject boulderGeneratorPrefab;

        PillarParameters.PillarDescriptor pillar;

        private void Start()
        {
            pillar = pillarParameters.pillars.FirstOrDefault(p => p.pillarId == pillarId);
            if (pillar != null)
            {
                BuildBoulderGenerators();
            }
        }

        private void BuildBoulderGenerators()
        {
            Vector3 center = transform.position;
            foreach (var boulderDescriptor in pillar.boulderGenerators)
            {
                GameObject o = Instantiate(boulderGeneratorPrefab);
                o.transform.position = center + boulderDescriptor.boulderGenerator;
                o.GetComponent<BoulderGenerator>().NextBoulderDelay = boulderDescriptor.boulderDelay;
            }
        }
    }
}
