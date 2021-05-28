using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterStateTrigger : MonoBehaviour, IStateTrigger
    {
        [SerializeField]
        private AnimatorTriggerName trigger;

        public void Trigger(Animator animator)
        {
            if (animator != null && trigger != null)
            {
                animator.SetTrigger(trigger.Hash);
            }
        }
    }
}
