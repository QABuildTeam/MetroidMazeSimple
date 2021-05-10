using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public abstract class StateSwitcher : StateMachineBehaviour
    {
        [Header("Animation parameters names")]
        [SerializeField]
        protected MetroidMazeModelControllerParameters parameterNames;
        [SerializeField]
        protected bool debugOutput = false;

        protected abstract MetroidMazeModelControllerParameters.AnimationParameter StateSwitchParameter { get; }
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(StateSwitchParameter.Hash, true);
            if (debugOutput)
            {
                Debug.Log($"[{Time.frameCount}] [{System.DateTime.Now:HH\\:mm\\:ss\\.fff}] [{GetType().Name}.{nameof(OnStateEnter)}] {StateSwitchParameter.name}={animator.GetBool(StateSwitchParameter.name)}");
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(StateSwitchParameter.Hash, false);
            if (debugOutput)
            {
                Debug.Log($"[{Time.frameCount}] [{System.DateTime.Now:HH\\:mm\\:ss\\.fff}] [{GetType().Name}.{nameof(OnStateExit)}] {StateSwitchParameter.name}={animator.GetBool(StateSwitchParameter.name)}");
            }
        }

    }
}
