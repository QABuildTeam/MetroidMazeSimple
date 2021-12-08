using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class JumpAnyStateController : CharacterStateController
    {
        [Header("Input events")]
        [SerializeField]
        private string horizontalInputAxis = "Horizontal";

        private float xMove = 0;
        public override void ProcessInput(Animator characterAnimator)
        {
        }

        public override void CheckState(Animator characterAnimator)
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Rigidbody characterRigidbody)
        {
            xMove = Input.GetAxis(horizontalInputAxis);
        }
        public override void Enter(Animator characterAnimator, Rigidbody characterRigidbody)
        {
            
        }
    }
}
