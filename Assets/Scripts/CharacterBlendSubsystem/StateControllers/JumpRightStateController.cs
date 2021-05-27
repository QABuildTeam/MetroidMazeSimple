using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class JumpRightStateController : CharacterStateController
    {
        [SerializeField]
        private AnimationStateName state;
        public override void CheckInput(Animator characterAnimator)
        {
        }

        public override void CheckState(Animator characterAnimator)
        {
            throw new System.NotImplementedException();
        }

        public override int Init(Animator characterAnimator, Rigidbody characterRigidbody)
        {
            if (state != null)
            {
                return state.Hash;
            }
            return 0;
        }

        public override void Move(Rigidbody characterRigidbody)
        {
            throw new System.NotImplementedException();
        }
    }
}
