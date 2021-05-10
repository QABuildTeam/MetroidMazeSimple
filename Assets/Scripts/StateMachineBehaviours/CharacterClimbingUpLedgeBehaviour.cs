using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterClimbingUpLedgeBehaviour : StateSwitcher
    {
        protected override MetroidMazeModelControllerParameters.AnimationParameter StateSwitchParameter => parameterNames.climbingUpLedge;
    }
}
