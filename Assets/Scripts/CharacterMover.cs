using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Character
{
    public abstract class CharacterMover : MonoBehaviour
    {
        public abstract void MoveCharacter(Vector3 moveDelta);
    }
}
