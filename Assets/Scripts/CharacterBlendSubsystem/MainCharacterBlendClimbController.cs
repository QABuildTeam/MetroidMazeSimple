using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetroidMaze.Utility;

namespace MetroidMaze.Character
{
    public class MainCharacterBlendClimbController : MonoBehaviour
    {
        [Header("Character components")]
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private Rigidbody characterRigidbody;
        [SerializeField]
        private ClimbUpCollider climbUpRightCollider;
        [SerializeField]
        private ClimbUpCollider climbUpLeftCollider;
        [SerializeField]
        private WallHangCollider wallHangRightCollider;
        [SerializeField]
        private WallHangCollider wallHangLeftCollider;
        [SerializeField]
        private GroundedCollider groundedCollider;
        [SerializeField]
        private Collider standingCollider;
        [SerializeField]
        private Collider climbingCollider;
        [SerializeField]
        private Collider climbingUpLedgeCollider;

        [Header("Animation parameters names")]
        [SerializeField]
        private MetroidMazeModelControllerParameters parameterNames;

        [Header("Input events")]
        [SerializeField]
        private string horizontalInputAxis = "Horizontal";
        [SerializeField]
        private string verticalInputAxis = "Vertical";
        [SerializeField]
        private string controlledStickButton = "joystick button 4";

        [Header("Tweaking parameters")]
        [SerializeField]
        private float climbingHorizontalSpeed = 1;
        [SerializeField]
        private float climbingVerticalSpeed = 1;
        [SerializeField]
        private Vector3 climbUpSpeed = Vector3.one;

        private float xMove = 0;
        private float yMove = 0;
        private bool stickOn = false;

        private Vector3 rightWallHangColliderDistance;
        private Vector3 leftWallHangColliderDistance;

        void SetColliders(bool climbing, bool climbingUpLedge, bool standing)
        {
            climbingCollider.enabled = climbing;
            climbingUpLedgeCollider.enabled = climbingUpLedge;
            //standingCollider.enabled = standing || climbing;
            standingCollider.enabled = standing;
        }

        private void Start()
        {
            SetColliders(climbing: false, climbingUpLedge: false, standing: true);
            rightWallHangColliderDistance = (climbingCollider.transform.position + climbingCollider.bounds.size / 2) - wallHangRightCollider.transform.position;
            leftWallHangColliderDistance = (climbingCollider.transform.position - climbingCollider.bounds.size / 2) - wallHangLeftCollider.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            xMove = Input.GetAxis(horizontalInputAxis);
            yMove = Input.GetAxis(verticalInputAxis);
            stickOn = Input.GetKey(controlledStickButton);
        }

        private bool hasBeenClimbingUp = false;

        private void FixedUpdate()
        {
            int faceDirection = characterAnimator.GetInteger(parameterNames.faceDirection.name);
            bool canStickToWall = (faceDirection == -1 && wallHangLeftCollider.CanHang) || (faceDirection == 1 && wallHangRightCollider.CanHang);
            bool canClimbUp = (faceDirection == -1 && climbUpLeftCollider.CanClimbUp) || (faceDirection == 1 && climbUpRightCollider.CanClimbUp);
            float wallDistance = canStickToWall ? (faceDirection == -1 ? leftWallHangColliderDistance.x + wallHangLeftCollider.Distance.x : (faceDirection == 1 ? rightWallHangColliderDistance.x + wallHangRightCollider.Distance.x : 0)) : 0;
            //Debug.Log($"faceDirection={faceDirection}, leftClimb={wallHangLeftCollider.CanHang}, rightClimb={wallHangRightCollider.CanHang}");

            void FloatCharacter()
            {
                characterRigidbody.useGravity = false;
                characterAnimator.SetFloat(parameterNames.horizontalVelocity.name, 0);
                characterAnimator.SetFloat(parameterNames.verticalVelocity.name, yMove);
                //Debug.Log($"horizontalVelocity={characterAnimator.GetFloat(parameterNames.horizontalVelocity.name)}, verticalVelocity={characterAnimator.GetFloat(parameterNames.verticalVelocity.name)}");
            }

            void MoveCharacter()
            {
                FloatCharacter();
                Vector3 snuggleDelta = new Vector3(wallDistance, 0, 0);
                Vector3 climbDelta = Vector3.up * Math.ZeroSign(yMove) * climbingVerticalSpeed * Time.fixedDeltaTime;
                // move character object
                Debug.Log($"yMove={yMove}, transform delta={climbDelta + snuggleDelta}");
                characterRigidbody.MovePosition(characterRigidbody.transform.position + climbDelta + snuggleDelta);
            }

            bool JumpAndHang(MetroidMazeModelControllerParameters.AnimationParameter parameter)
            {
                if (characterAnimator.GetBool(parameter.name))
                {
                    //Debug.Log("Jump and hang trigger");
                    //Debug.Log($"faceDirection={faceDirection}, leftClimb={wallHangLeftCollider.CanHang}, rightClimb={wallHangRightCollider.CanHang}, canStickToWall={canStickToWall}, stickOn={stickOn}");
                    characterAnimator.SetBool(parameter.name, false);
                    SetColliders(climbing: true, climbingUpLedge: false, standing: false);
                    FloatCharacter();
                    characterAnimator.SetBool(parameterNames.jumpAndHanging.name, true);
                    characterAnimator.SetTrigger(parameterNames.jumpAndHang.name);
                    return true;
                }
                return false;
            }

            void ClimbToWalk()
            {
                Debug.Log("Climb to walk trigger");
                Debug.Log($"faceDirection={faceDirection}, leftClimb={wallHangLeftCollider.CanHang}, rightClimb={wallHangRightCollider.CanHang}, canStickToWall={canStickToWall}, stickOn={stickOn}");
                characterAnimator.SetBool(parameterNames.climbing.name, false);
                characterRigidbody.useGravity = true;
                SetColliders(climbing: false, climbingUpLedge: false, standing: true);
                characterAnimator.SetTrigger(parameterNames.climbToWalk.name);
            }

            if (!hasBeenClimbingUp)
            {
                // special handling for climbUpLedge animation
                if (characterAnimator.GetBool(parameterNames.climbingUpLedge.name))
                {
                    Debug.Log("Climbing up...");
                    Debug.Log($"faceDirection={faceDirection}, leftClimb={wallHangLeftCollider.CanHang}, rightClimb={wallHangRightCollider.CanHang}, canStickToWall={canStickToWall}, stickOn={stickOn}");
                    SetColliders(climbing: false, climbingUpLedge: true, standing: false);
                    hasBeenClimbingUp = true;
                    characterRigidbody.useGravity = false;
                    Vector3 moveDelta = new Vector3(climbUpSpeed.x * faceDirection, climbUpSpeed.y, climbUpSpeed.z) * Time.fixedDeltaTime;
                    characterRigidbody.MovePosition(characterRigidbody.transform.position + moveDelta);
                    return;
                }
            }
            else
            {
                Debug.Log("...finished climbing up");
                Debug.Log($"faceDirection={faceDirection}, leftClimb={wallHangLeftCollider.CanHang}, rightClimb={wallHangRightCollider.CanHang}, canStickToWall={canStickToWall}, stickOn={stickOn}");
                hasBeenClimbingUp = false;
                SetColliders(climbing: false, climbingUpLedge: false, standing: true);
                characterRigidbody.useGravity = true;
            }
            if (canStickToWall && stickOn)
            {
                if (characterAnimator.GetBool(parameterNames.climbing.name))
                {
                    // climbing
                    if(yMove > float.Epsilon&& canClimbUp)
                    {
                        // climb up
                        Debug.Log("Climb up trigger");
                        characterAnimator.SetBool(parameterNames.climbing.name, false);
                        characterAnimator.SetTrigger(parameterNames.climbUpLedge.name);
                    }
                    else
                    {
                        MoveCharacter();
                    }
                }
                else
                {
                    if (JumpAndHang(parameterNames.jumping) ||
                        JumpAndHang(parameterNames.falling) ||
                        JumpAndHang(parameterNames.standing))
                    {

                    }
                }
            }
            else
            {
                //Debug.Log($"canStickToWall={canStickToWall}, stickOn={stickOn}");
                if (characterAnimator.GetBool(parameterNames.climbing.name))
                {
                    ClimbToWalk();
                }
            }
        }
    }
}
