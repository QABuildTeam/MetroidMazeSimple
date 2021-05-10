using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    public class BoulderGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject boulderPrefab;
        [SerializeField]
        private Vector3 boulderShift = new Vector3(0, 2, 0);

        public float NextBoulderDelay { get; set; } = 2f;

        private void Start()
        {
            StartCoroutine(GenerateBoulders());
        }

        private IEnumerator GenerateBoulders()
        {
            while (true)
            {
                float currentTime = 0;
                while (currentTime < NextBoulderDelay)
                {
                    yield return null;
                    currentTime += Time.deltaTime;
                }
                var boulder = Instantiate(boulderPrefab);
                var position = transform.position;
                boulder.transform.position = position + boulderShift;
            }
        }
    }
}
