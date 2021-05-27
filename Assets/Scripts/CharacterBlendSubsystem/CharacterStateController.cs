using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterStateController : MonoBehaviour, IStateController
    {
        [SerializeField]
        private AnimationStateName state;
        [SerializeField]
        private bool initial = false;

        public bool Active => true;

        public bool Initial => initial;

        public virtual void CheckInput(Animator characterAnimator) { }

        public virtual void CheckState(Animator characterAnimator) { }

        public virtual int Init(Animator characterAnimator, Rigidbody characterRigidbody)
        {
            return state != null ? state.Hash : 0;
        }

        public virtual void Move(Rigidbody characterRigidbody) { }
    }
}
