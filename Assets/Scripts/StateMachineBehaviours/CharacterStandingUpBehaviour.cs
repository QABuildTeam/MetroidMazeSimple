using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterStandingUpBehaviour : StateSwitcher
    {
        protected override MetroidMazeModelControllerParameters.AnimationParameter StateSwitchParameter => parameterNames.laying;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}
