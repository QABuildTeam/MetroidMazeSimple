using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public interface IStatusController
    {
        bool Active { get; }
        void Init(Animator characterAnimator, Rigidbody characterRigidbody);
        void CheckInput(Animator characterAnimator);
        void CheckStatus(Animator characterAnimator);
        void Move(Rigidbody characterRigidbody);
    }
}
