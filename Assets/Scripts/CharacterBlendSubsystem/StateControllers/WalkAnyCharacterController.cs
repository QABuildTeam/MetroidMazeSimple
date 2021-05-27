using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class WalkAnyCharacterController : CharacterStateController
    {
        [Header("Animation parameters names")]
        [SerializeField]
        private MetroidMazeModelControllerParameters parameters;
        [Header("Colliders")]
        [SerializeField]
        private GroundedCollider groundedCollider;
        [Header("Input events")]
        [SerializeField]
        private string horizontalInputAxis = "Horizontal";
        [Header("Tweaking parameters")]
        [SerializeField]
        private float horizontalSpeedFactor = 4;

        private float xMove = 0;
        public override int Init(Animator characterAnimator, Rigidbody characterRigidbody)
        {
            characterAnimator.SetInteger(parameters.faceDirection.Hash, 1);
            // the character is in the air now, so make him fall onto the surface
            characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, 0);
            characterAnimator.SetTrigger(parameters.fall.Hash);
            return base.Init(characterAnimator, characterRigidbody);
        }

        public override void CheckInput(Animator characterAnimator)
        {
            xMove = Input.GetAxis(horizontalInputAxis);
        }

        public override void CheckState(Animator characterAnimator)
        {
            characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, xMove);
        }

        public override void Move(Rigidbody characterRigidbody)
        {
            Vector3 moveDelta = Vector3.right * Time.fixedDeltaTime * xMove * horizontalSpeedFactor;
            // move character object
            characterRigidbody.MovePosition(characterRigidbody.transform.position + moveDelta);
        }
    }
}
