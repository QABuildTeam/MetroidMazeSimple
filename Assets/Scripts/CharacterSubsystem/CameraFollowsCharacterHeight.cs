using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    public class CameraFollowsCharacterHeight : MonoBehaviour
    {
        [SerializeField]
        private GameObject character;

        private Transform cameraTransform;
        private Transform characterTransform;
        private float originalDelta;

        private void Awake()
        {
            cameraTransform = transform;
            characterTransform = character.transform;
            originalDelta = cameraTransform.position.y - characterTransform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 characterPosition = characterTransform.position;
            Vector3 cameraPosition = cameraTransform.position;
            cameraPosition.y = characterPosition.y + originalDelta;
            cameraTransform.position = cameraPosition;
        }
    }
}
