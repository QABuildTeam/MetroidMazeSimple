using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class AnimatorStateSwitcher : StateMachineBehaviour
    {
        [SerializeField]
        protected bool debugOutput = false;
        [SerializeField]
        protected AnimationStateName state;

        protected IStateContainer stateContainer = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateContainer = animator.GetComponent<IStateContainer>();
            if (stateContainer != null)
            {
                stateContainer.OnStateEnter(state.Hash);
                if (debugOutput)
                {
                    Debug.Log($"[{Time.frameCount}] [{System.DateTime.Now:HH\\:mm\\:ss\\.fff}] [{GetType().Name}.{nameof(OnStateEnter)}] Entered state {state.name}");
                }
            }
            else
            {
                Debug.LogError($"[{Time.frameCount}] [{System.DateTime.Now:HH\\:mm\\:ss\\.fff}] [{GetType().Name}.{nameof(OnStateEnter)}] No state container defined");
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateContainer = animator.GetComponent<IStateContainer>();
            if (stateContainer != null)
            {
                stateContainer.OnStateExit(state.Hash);
                if (debugOutput)
                {
                    Debug.Log($"[{Time.frameCount}] [{System.DateTime.Now:HH\\:mm\\:ss\\.fff}] [{GetType().Name}.{nameof(OnStateEnter)}] Exited state {state.name}");
                }
            }
            else
            {
                Debug.LogError($"[{Time.frameCount}] [{System.DateTime.Now:HH\\:mm\\:ss\\.fff}] [{GetType().Name}.{nameof(OnStateEnter)}] No state container defined");
            }
        }

    }
}
