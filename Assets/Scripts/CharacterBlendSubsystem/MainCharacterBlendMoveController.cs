using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class MainCharacterBlendMoveController : MonoBehaviour
    {
        [Header("Character components")]
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private Rigidbody characterRigidbody;
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

        private void Start()
        {
            // the character is looking right
            characterAnimator.SetInteger(parameters.faceDirection.Hash, 1);
            // the character is in the air now, so make him fall onto the surface
            characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, 0);
            characterAnimator.SetBool(parameters.grounded.Hash, false);
            characterAnimator.SetBool(parameters.standing.Hash, false);
            characterAnimator.SetTrigger(parameters.fall.Hash);
        }

        private float xMove = 0;

        private void Update()
        {
            xMove = Input.GetAxis(horizontalInputAxis);
        }

        private void FixedUpdate()
        {
            float horizontalFactor = horizontalSpeedFactor;
            characterAnimator.SetBool(parameters.grounded.Hash, groundedCollider.IsGrounded);
            //_ = groundedCollisionDetector.IsGrounded;
            //Debug.Log($"Grounded collision={groundedCollisionDetector.IsGrounded}");

            void MoveCharacter()
            {
                Vector3 moveDelta = Vector3.right * Time.fixedDeltaTime * xMove * horizontalFactor;
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

            if (characterAnimator.GetBool(parameters.grounded.Hash))
            {
                if (characterAnimator.GetBool(parameters.standing.Hash) || characterAnimator.GetBool(parameters.landing.Hash))
                {
                    characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, xMove);
                    MoveCharacter();
                }
                else if (characterAnimator.GetBool(parameters.falling.Hash))
                {
                    Debug.Log($"Falling -> Landing");
                    characterAnimator.SetBool(parameters.falling.Hash, false);
                    characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, 0);
                    characterAnimator.SetBool(parameters.landing.Hash, true);
                    characterAnimator.SetTrigger(parameters.land.Hash);
                    MoveCharacter();
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
                    MoveCharacter();
                }
                else if (characterAnimator.GetBool(parameters.falling.Hash))
                {
                    MoveCharacter();
                }
            }
        }
    }
}
