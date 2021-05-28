using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public interface IStateTrigger
    {
        void Trigger(Animator animator);
    }

    public interface IStateTrigger<T>
    {
        void Trigger(Animator animator, T arg);
    }
}
