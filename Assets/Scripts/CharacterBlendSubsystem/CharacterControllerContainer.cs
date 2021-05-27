using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterControllerContainer : MonoBehaviour, IStateContainer
    {
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private Rigidbody characterRigidbody;

        private Dictionary<int, IStateController> controllers = new Dictionary<int, IStateController>();
        private int currentStateHash = 0;

        public void OnStateEnter(int animationStateNameHash)
        {
            if (controllers.ContainsKey(animationStateNameHash))
            {
                currentStateHash = animationStateNameHash;
            }
        }

        public void OnStateExit(int animationStateNameHash)
        {
        }

        private void Awake()
        {
            foreach (var controller in GetComponentsInChildren<IStateController>())
            {
                int stateHash = controller.Init(characterAnimator, characterRigidbody);
                if (stateHash != 0 && !controllers.ContainsKey(stateHash))
                {
                    controllers[stateHash] = controller;
                    if (controller.Initial)
                    {
                        currentStateHash = stateHash;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (controllers.ContainsKey(currentStateHash))
            {
                var currentController = controllers[currentStateHash];
                currentController.CheckInput(characterAnimator);
                currentController.CheckState(characterAnimator);
                currentController.Move(characterRigidbody);
            }
        }
    }
}
