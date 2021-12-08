using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public interface IStateTrigger
    {
        bool Active { get; set; }
        void Trigger(Animator animator);
    }

    public interface IStateTrigger<T>
    {
        bool Active { get; set; }
        void Trigger(Animator animator, T arg);
    }
}
