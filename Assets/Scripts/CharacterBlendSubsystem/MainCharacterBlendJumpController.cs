using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class MainCharacterBlendJumpController : MonoBehaviour
    {
        [Header("Character components")]
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private Rigidbody characterRigidbody;
        [SerializeField]
        private CharacterMover characterMover;

        [Header("Animation parameters names")]
        [SerializeField]
        private MetroidMazeModelControllerParameters parameters;

        [Header("Input events")]
        [SerializeField]
        private string horizontalInputAxis = "Horizontal";
        [SerializeField]
        private string verticalInputAxis = "Vertical";
        [SerializeField]
        private string controlledJumpButton = "joystick button 0";

        [Header("Tweaking parameters")]
        [SerializeField]
        private float horizontalFactor = 1;
        [SerializeField]
        private float verticalJumpFactor = 0.25f;
        [SerializeField]
        private int maxJumpCount = 3;

        private float xMove = 0;
        private bool jump = false;

        private void Awake()
        {
            characterAnimator.SetBool(parameters.jumping.Hash, false);
        }

        // Update is called once per frame
        private void Update()
        {
            xMove = Input.GetAxis(horizontalInputAxis);
            jump = jump || Input.GetKeyDown(controlledJumpButton) || Input.GetButtonDown("Jump");
            if (jump)
            {
                //Debug.Log($"[{GetType().Name}.{nameof(Update)}] jump={jump}");
            }
        }

        private void FixedUpdate()
        {
            bool jumpPressed = jump;
            bool startJumping = false;
            jump = false;
            int jumpCounter = characterAnimator.GetInteger(parameters.jumpCounter.Hash);
            bool grounded = characterAnimator.GetBool(parameters.grounded.Hash);

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
            }

            if (jumpPressed)
            {
                if (grounded)
                {
                    if (characterAnimator.GetBool(parameters.standing.Hash))
                    {
                        Debug.Log($"[{System.DateTime.Now}] First jump");
                        // this can be the first jump
                        startJumping = true;
                    }
                }
                else
                {
                    Debug.Log($"Jumping={characterAnimator.GetBool(parameters.jumping.Hash)}, falling ={characterAnimator.GetBool(parameters.falling.Hash)}, jumpCounter={jumpCounter}");
                    // this can be the next jump
                    if (characterAnimator.GetBool(parameters.jumping.Hash) ||
                        (characterAnimator.GetBool(parameters.falling.Hash) &&
                        jumpCounter > 0))
                    {
                        startJumping = true;
                    }
                }
            }
            if (startJumping && jumpCounter < maxJumpCount)
            {
                Debug.Log($"[{System.DateTime.Now}] Jump up");
                characterAnimator.SetBool(parameters.standing.Hash, false);
                characterAnimator.SetBool(parameters.jumping.Hash, true);
                characterAnimator.SetTrigger(parameters.jump.Hash);
                characterAnimator.SetFloat(parameters.horizontalVelocity.Hash, xMove);
                characterAnimator.SetInteger(parameters.jumpCounter.Hash, jumpCounter + 1);
                MoveCharacter();
                characterRigidbody.AddForce(Vector3.up * verticalJumpFactor, ForceMode.Impulse);
            }
            else
            {
                if (characterAnimator.GetBool(parameters.jumping.Hash))
                {
                    MoveCharacter();
                }
                else if (grounded)
                {
                    characterAnimator.SetInteger(parameters.jumpCounter.Hash, 0);
                }
            }
        }
    }
}
