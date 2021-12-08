using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public interface IStateController
    {
        bool Initial { get; }
        bool Active { get; }
        /// <summary>
        /// Initialize animation status controller to handle its animation state
        /// </summary>
        /// <param name="characterAnimator">Animator component to interact with</param>
        /// <param name="characterRigidbody">Rigidbody component to interact with</param>
        /// <returns>Integer hash of the controller's animation state</returns>
        int Init(Animator characterAnimator, Rigidbody characterRigidbody);
        /// <summary>
        /// This method is called upon entering the state
        /// </summary>
        /// <param name="characterAnimator">Animator component to interact with</param>
        /// <param name="characterRigidbody">Rigidbody component to interact with</param>
        void Enter(Animator characterAnimator, Rigidbody characterRigidbody);
        void ProcessInput(Animator characterAnimator);
        void CheckState(Animator characterAnimator);
        void Move(Rigidbody characterRigidbody);
        /// <summary>
        /// This method is called upon exiting the state
        /// </summary>
        /// <param name="characterAnimator">Animator component to interact with</param>
        /// <param name="characterRigidbody">Rigidbody component to interact with</param>
        void Exit(Animator characterAnimator, Rigidbody characterRigidbody);
    }
}
