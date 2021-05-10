using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CameraFollowsCharacter : MonoBehaviour
    {
        [SerializeField]
        private GameObject character;
        [SerializeField]
        private Vector3 cameraDelta;

        // Update is called once per frame
        void Update()
        {
            Vector3 localPosition = character.transform.localPosition;
            //localPosition.z = transform.localPosition.z;
            localPosition += cameraDelta;
            transform.localPosition = localPosition;
        }
    }
}
