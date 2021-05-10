using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    [CreateAssetMenu(fileName = "PillarParameters", menuName = "Pillar Rolling Settings/Pillar Parameters", order = 56)]
    public class PillarParameters : ScriptableObject
    {
        [System.Serializable]
        public class PillarDescriptor
        {
            public int pillarId;
            public List<StairsDescriptor> stairs;
            public List<BoulderDescriptor> boulderGenerators;
        }
        public List<PillarDescriptor> pillars;
    }
}
