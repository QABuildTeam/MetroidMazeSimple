using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze
{
    [CreateAssetMenu(fileName = "MetroidMazeModelControllerParameters", menuName = "Metroid Maze Settings/Animation Parameters", order = 55)]
    public class MetroidMazeModelControllerParameters : ScriptableObject
    {
        [System.Serializable]
        public class AnimationParameter
        {
            public string name;
            private int hash = 0;
            public int Hash
            {
                get
                {
                    if (hash == 0)
                    {
                        hash = Animator.StringToHash(name);
                    }
                    return hash;
                }
            }
            public AnimatorControllerParameterType type;
        }

        // ====================================================
        // velocity parameters
        public AnimationParameter horizontalVelocity;   // float
        public AnimationParameter verticalVelocity;     // float
        // ====================================================
        // commands
        public AnimationParameter turn180;              // trigger
        public AnimationParameter jump;                 // trigger
        public AnimationParameter nextJump;             // trigger
        public AnimationParameter fall;                 // trigger
        public AnimationParameter land;                 // trigger
        public AnimationParameter immediateBreak;       // trigger
        public AnimationParameter layDown;              // trigger
        public AnimationParameter standUp;              // trigger
        public AnimationParameter jumpAndHang;          // trigger
        public AnimationParameter hopUpClimb;           // trigger
        public AnimationParameter climbUpLedge;         // trigger
        public AnimationParameter climb;                // trigger
        public AnimationParameter stand;                // trigger
        public AnimationParameter climbToWalk;          // trigger
        // ====================================================
        // status
        public AnimationParameter grounded;             // bool
        public AnimationParameter standing;             // bool
        public AnimationParameter jumping;              // bool
        public AnimationParameter jumpCounter;          // int
        public AnimationParameter nextJumping;          // bool
        public AnimationParameter turning;              // bool
        public AnimationParameter laying;               // bool
        public AnimationParameter climbing;             // bool
        public AnimationParameter falling;              // bool
        public AnimationParameter landing;              // bool
        public AnimationParameter crawling;             // bool
        public AnimationParameter faceDirection;        // int
        public AnimationParameter climbingUpLedge;      // bool
        public AnimationParameter jumpAndHanging;       // bool
        public AnimationParameter climbingToWalk;       // bool
    }
}
