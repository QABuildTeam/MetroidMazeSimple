using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterControllerProxy : MonoBehaviour, IStateContainer
    {
        [SerializeField]
        private CharacterControllerContainer controllerContainer;

        public void OnStateEnter(int animationStateNameHash)
        {
            if (controllerContainer != null)
            {
                controllerContainer.OnStateEnter(animationStateNameHash);
            }
        }

        public void OnStateExit(int animationStateNameHash)
        {
            if (controllerContainer != null)
            {
                controllerContainer.OnStateExit(animationStateNameHash);
            }
        }
    }
}
