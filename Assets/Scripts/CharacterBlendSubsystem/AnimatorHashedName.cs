using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace MetroidMaze.Character
{
    public abstract class AnimatorHashedName
    {
#if UNITY_EDITOR
        public AnimatorController animatorController = null;
#endif
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
