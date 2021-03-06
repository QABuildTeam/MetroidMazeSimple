using Utility.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using MetroidMaze.Character;

namespace MetroidMaze.Editor
{
    [CustomPropertyDrawer(typeof(AnimationStateName))]
    public class AnimationStateNamePropertyDrawer : AnimatorHashedNameProperyDrawer
    {
        protected override List<string> AnimatorNames(AnimatorController animatorController)
        {
            List<string> animationNames = new List<string>();
            if (animatorController != null)
            {
                var layers = animatorController.layers;
                animationNames = layers[0].stateMachine.states.Select(s => s.state.name).ToList();
            }
            return animationNames;
        }
    }
}
