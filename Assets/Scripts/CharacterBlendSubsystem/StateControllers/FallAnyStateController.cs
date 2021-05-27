using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class FallAnyStateController : CharacterStateController
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

        public override void CheckState(Animator characterAnimator)
        {
            if (groundedCollider.IsGrounded)
            {
                characterAnimator.SetTrigger(parameters.land.Hash);
            }
        }
    }
}
