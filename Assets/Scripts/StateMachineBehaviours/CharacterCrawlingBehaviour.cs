using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class CharacterCrawlingBehaviour : StateSwitcher
    {
        protected override MetroidMazeModelControllerParameters.AnimationParameter StateSwitchParameter => parameterNames.crawling;
    }
}
