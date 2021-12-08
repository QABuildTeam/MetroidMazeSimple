using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterStateTrigger : MonoBehaviour, IStateTrigger
    {
        [SerializeField]
        private AnimatorTriggerName trigger;
        [SerializeField]
        private bool active = true;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        public void Trigger(Animator animator)
        {
            if (animator != null && trigger != null && Active)
            {
                animator.SetTrigger(trigger.Hash);
            }
        }
    }
}
