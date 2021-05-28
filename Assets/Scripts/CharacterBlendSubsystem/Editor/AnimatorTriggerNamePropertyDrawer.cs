using Utility.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using MetroidMaze.Character;

namespace MetroidMaze.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorTriggerName))]
    public class AnimatorTriggerNamePropertyDrawer : AnimatorHashedNameProperyDrawer
    {
        protected override List<string> AnimatorNames(AnimatorController animatorController)
        {
            List<string> animationNames = new List<string>();
            if (animatorController != null)
            {
                animationNames = animatorController.parameters.Where(p => p.type == AnimatorControllerParameterType.Trigger).Select(p => p.name).ToList();
            }
            return animationNames;
        }
    }
}
