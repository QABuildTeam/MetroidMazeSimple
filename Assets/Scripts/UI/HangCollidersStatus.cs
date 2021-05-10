using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MetroidMaze.Character;

namespace MetroidMaze.UI
{
    public class HangCollidersStatus : MonoBehaviour
    {
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private MetroidMazeModelControllerParameters parameters;
        [SerializeField]
        private WallHangCollider leftHangCollider;
        [SerializeField]
        private WallHangCollider rightHangCollider;
        [SerializeField]
        private TextMeshProUGUI currentStatusText;

        private string currentStatus;

        private void FixedUpdate()
        {
            string newStatus =
                $"leftClimb={leftHangCollider.CanHang}" +
                $"\nrightClimb={rightHangCollider.CanHang}" +
                $"\nfaceDirection={characterAnimator.GetInteger(parameters.faceDirection.name)}";
            if (currentStatus != newStatus)
            {
                currentStatus = newStatus;
                currentStatusText.text = currentStatus;
            }
        }
    }
}
