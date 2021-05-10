using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    public class PillarBuilder : MonoBehaviour
    {
        [SerializeField]
        private PillarParameters parameters;
        [SerializeField]
        private GameObject pillarPrefab;

        // Start is called before the first frame update
        void Start()
        {
            foreach(var descriptor in parameters.pillars)
            {
                BuildPillar(descriptor);
            }
        }

        private void BuildPillar(PillarParameters.PillarDescriptor descriptor)
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
