using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using MetroidMaze.Utility;

namespace MetroidMaze.Character
{
    public class MainCharacterBlendCrawlController : MonoBehaviour
    {
        [Header("Character components")]
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private Rigidbody characterRigidbody;
        [SerializeField]
        private GroundedCollider groundedCollider;
        [SerializeField]
        private CrawlCollider leftCrawlCollider;
        [SerializeField]
        private CrawlCollider rightCrawlCollider;
        [SerializeField]
        private Collider standingCollider;
        [SerializeField]
        private Collider layingCollider;
        [SerializeField]
        private StandingObstacle standingObstacle;

        [Header("Animation parameters names")]
        [SerializeField]
        private MetroidMazeModelControllerParameters parameterNames;

        [Header("Input events")]
        [SerializeField]
        private string horizontalInputAxis = "Horizontal";
        [SerializeField]
        private string verticalInputAxis = "Vertical";

        [Header("Tweaking parameters")]
        [SerializeField]
        private float horizontalSpeed = 1;
        [SerializeField]
        private float horizontalSpeedThreshold = 0.1f;
        [SerializeField]
        private float verticalThreshold = 0.25f;

        private float xMove = 0;
        private float yMove = 0;

        private void Awake()
        {
            characterAnimator.SetBool(parameterNames.laying.name, false);
            standingCollider.enabled = true;
            layingCollider.enabled = false;
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            xMove = Input.GetAxis(horizontalInputAxis);
            yMove = Input.GetAxis(verticalInputAxis);
        }

        private void FixedUpdate()
        {
            if (characterAnimator.GetBool(parameterNames.grounded.name))
            {
                if (characterAnimator.GetBool(parameterNames.standing.name))
                {
                    if (yMove < -verticalThreshold)
                    {
                        Debug.Log($"Standing -> Laying");
                        characterAnimator.SetFloat(parameterNames.horizontalVelocity.name, 0);
                        characterAnimator.SetTrigger(parameterNames.layDown.name);
                        standingCollider.enabled = false;
                        layingCollider.enabled = true;
                    }
                }
                else if (characterAnimator.GetBool(parameterNames.crawling.name))
                {
                    if (yMove > verticalThreshold && standingObstacle.CanStandUp)
                    {
                        Debug.Log($"Laying -> Standing");
                        characterAnimator.SetFloat(parameterNames.horizontalVelocity.name, 0);
                        characterAnimator.SetBool(parameterNames.crawling.name, false);
                        characterAnimator.SetTrigger(parameterNames.standUp.name);
                        standingCollider.enabled = true;
                        layingCollider.enabled = false;
                    }
                    else
                    {
                        Debug.Log($"Cannot stand up");
                        characterAnimator.SetFloat(parameterNames.horizontalVelocity.name, xMove);
                        if (Mathf.Abs(xMove) > horizontalSpeedThreshold)
                        {
                            if ((xMove > 0 && rightCrawlCollider.CanCrawl) || (xMove < 0 && leftCrawlCollider.CanCrawl))
                            {
                                // move character object
                                characterRigidbody.MovePosition(characterRigidbody.transform.position + Vector3.right * Time.fixedDeltaTime * Math.ZeroSign(xMove) * horizontalSpeed);
                            }
                        }
                    }
                }
            }
        }
    }
}
