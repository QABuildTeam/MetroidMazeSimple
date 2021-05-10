using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterTurn180Behaviour : StateSwitcher
    {
        [SerializeField]
        private int faceDirectionOnExit = 0;

        protected override MetroidMazeModelControllerParameters.AnimationParameter StateSwitchParameter => parameterNames.turning;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetInteger(parameterNames.faceDirection.name, 0);
            //Debug.Log($"[{GetType().Name}.{nameof(OnStateEnter)}] {parameterNames.faceDirection.name}={animator.GetInteger(parameterNames.faceDirection.name)}");
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            animator.SetInteger(parameterNames.faceDirection.name, faceDirectionOnExit);
            //Debug.Log($"[{GetType().Name}.{nameof(OnStateExit)}] {parameterNames.faceDirection.name}={animator.GetInteger(parameterNames.faceDirection.name)}");
        }
    }
}
