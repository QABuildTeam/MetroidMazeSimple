using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterControllerContainer : MonoBehaviour
    {
        private IStatusController[] controllers;
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private Rigidbody characterRigidbody;

        private void Awake()
        {
            controllers = GetComponentsInChildren<IStatusController>();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < controllers.Length; ++i)
            {
                if (controllers[i].Active)
                {
                    controllers[i].CheckInput(characterAnimator);
                }
            }
            for (int i = 0; i < controllers.Length; ++i)
            {
                if (controllers[i].Active)
                {
                    controllers[i].CheckStatus(characterAnimator);
                }
            }
            for (int i = 0; i < controllers.Length; ++i)
            {
                if (controllers[i].Active)
                {
                    controllers[i].Move(characterRigidbody);
                }
            }
        }
    }
}
