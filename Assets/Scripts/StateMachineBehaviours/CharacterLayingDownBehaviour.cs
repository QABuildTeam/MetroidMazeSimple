using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    /*
    public class CharacterLayingDownBehaviour : StateMachineBehaviour
    {
        [Header("Animation parameters names")]
        [SerializeField]
        private MetroidMazeModelControllerParameters parameterNames;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(parameterNames.laying.name, true);
        }
    }
    */
    public class CharacterLayingDownBehaviour : StateSwitcher
    {
        protected override MetroidMazeModelControllerParameters.AnimationParameter StateSwitchParameter => parameterNames.laying;
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}
