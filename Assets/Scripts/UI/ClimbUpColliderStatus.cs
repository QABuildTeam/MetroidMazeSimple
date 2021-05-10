using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MetroidMaze.Character;

namespace MetroidMaze.UI
{
    public class ClimbUpColliderStatus : MonoBehaviour
    {
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private MetroidMazeModelControllerParameters parameters;
        [SerializeField]
        private ClimbUpCollider leftClimbUpCollider;
        [SerializeField]
        private ClimbUpCollider rightClimbUpCollider;
        [SerializeField]
        private TextMeshProUGUI currentStatusText;

        private string currentStatus;

        private void FixedUpdate()
        {
            string newStatus =
                $"leftClimbUp={leftClimbUpCollider.CanClimbUp}" +
                $"\nrightClimbUp={rightClimbUpCollider.CanClimbUp}" +
                $"\nfaceDirection={characterAnimator.GetInteger(parameters.faceDirection.name)}";
            if (currentStatus != newStatus)
            {
                currentStatus = newStatus;
                currentStatusText.text = currentStatus;
            }
        }
    }
}
