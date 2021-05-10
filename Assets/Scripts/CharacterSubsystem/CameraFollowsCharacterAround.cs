using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    public class CameraFollowsCharacterAround : MonoBehaviour
    {
        [SerializeField]
        private GameObject character;
        [SerializeField]
        private GameObject pillar;
        [SerializeField]
        private Vector3 cameraDelta;

        private Transform cameraTransform;
        private Transform characterTransform;
        private Transform pillarTransform;

        private void Awake()
        {
            cameraTransform = transform;
            characterTransform = character.transform;
            pillarTransform = pillar.transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 characterPosition = characterTransform.position;
            Vector3 pillarPosition = pillarTransform.position;
            Vector3 cameraDirection = (cameraTransform.position - pillarPosition).normalized;
            Vector3 characterDirection = (characterPosition - pillarPosition).normalized;
            Vector3 scaledCameraDelta = Vector3.Scale(characterDirection, cameraDelta);
            cameraTransform.RotateAround(pillarTransform.position, Vector3.down, Vector3.SignedAngle(cameraDirection, characterDirection, Vector3.down));
            cameraTransform.position = characterPosition + scaledCameraDelta;
        }
    }
}
