using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace MetroidMaze.Character
{
    [Serializable]
    public class AnimationStateName
    {
        [SerializeField]
        public AnimatorController animatorController = null;
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
    }
}
