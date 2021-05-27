using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public interface IStateContainer
    {
        void OnStateEnter(int animationStateNameHash);
        void OnStateExit(int animationStateNameHash);
    }
}
