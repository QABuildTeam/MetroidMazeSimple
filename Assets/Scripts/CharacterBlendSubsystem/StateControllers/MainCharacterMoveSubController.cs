using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class MainCharacterMoveSubController : MonoBehaviour
    {
        [Header("Character components")]
        [SerializeField]
        private GroundedCollider groundedCollider;
        [SerializeField]
        private CharacterMover characterMover;
        /*
        [SerializeField]
        private GroundedCollisionDetector groundedCollisionDetector;
        */

        [Header("Animation parameters names")]
        [SerializeField]
        private MetroidMazeModelControllerParameters parameters;

        [Header("Input events")]
        [SerializeField]
        private string horizontalInputAxis = "Horizontal";
        [SerializeField]
        private string verticalInputAxis = "Vertical";

        [Header("Tweaking parameters")]
        [SerializeField]
        private float horizontalSpeedFactor = 4;

        // Input
        private float xMove = 0;
        private bool grounded = false;
        private bool move = false;

        [SerializeField]
        private bool active;
        public bool Active => active;

        private void Update()
        {
            xMove = Input.GetAxis(horizontalInputAxis);
        }


        public void CheckInput(Animator characterAnimator)
        {
            xMove = Input.GetAxis(horizontalInputAxis);
            move = Mathf.Abs(xMove) > float.Epsilon;
            grounded = groundedCollider.IsGrounded;
            characterAnimator.SetBool(parameters.grounded.Hash, grounded);
            characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, xMove);
        }

        public void CheckState(Animator characterAnimator)
        {
            move = false;
            bool grounded = groundedCollider.IsGrounded;
            characterAnimator.SetBool(parameters.grounded.Hash, grounded);
            if (grounded)
            {
                characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, xMove);
            }
            //_ = groundedCollisionDetector.IsGrounded;
            //Debug.Log($"Grounded collision={groundedCollisionDetector.IsGrounded}");

            if (characterAnimator.GetBool(parameters.grounded.Hash))
            {
                if (characterAnimator.GetBool(parameters.standing.Hash) || characterAnimator.GetBool(parameters.landing.Hash))
                {
                    characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, xMove);
                    move = true;
                }
                else if (characterAnimator.GetBool(parameters.falling.Hash))
                {
                    Debug.Log($"Falling -> Landing");
                    characterAnimator.SetBool(parameters.falling.Hash, false);
                    characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, 0);
                    characterAnimator.SetBool(parameters.landing.Hash, true);
                    characterAnimator.SetTrigger(parameters.land.Hash);
                    move = true;
                }
            }
            else
            {
                if (characterAnimator.GetBool(parameters.standing.Hash))
                {
                    Debug.Log($"Standing -> Falling");
                    characterAnimator.SetBool(parameters.standing.Hash, false);
                    characterAnimator.SetBool(parameters.falling.Hash, true);
                    characterAnimator.SetTrigger(parameters.fall.Hash);
                    move = true;
                }
                else if (characterAnimator.GetBool(parameters.falling.Hash))
                {
                    move = true;
                }
            }
        }

        public void Move(Rigidbody characterRigidbody)
        {
            if (move)
            {
                Vector3 moveDelta = Vector3.right * Time.fixedDeltaTime * xMove * horizontalSpeedFactor;
                // move character object
                if (characterMover != null)
                {
                    characterMover.MoveCharacter(moveDelta);
                }
                else
                {
                    characterRigidbody.MovePosition(characterRigidbody.transform.position + moveDelta);
                }
                /*
                // move character object
                characterRigidbody.MovePosition(characterRigidbody.transform.position + Vector3.right * Time.fixedDeltaTime * xMove * horizontalFactor);
                */
            }
        }

        public int Init(Animator characterAnimator, Rigidbody characterRigidbody)
        {
            // the character is looking right
            characterAnimator.SetInteger(parameters.faceDirection.Hash, 1);
            // the character is in the air now, so make him fall onto the surface
            characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, 0);
            characterAnimator.SetBool(parameters.grounded.Hash, false);
            characterAnimator.SetBool(parameters.standing.Hash, false);
            characterAnimator.SetTrigger(parameters.fall.Hash);
            return 0;
        }
    }
}
