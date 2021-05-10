using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze.Utility
{
    public static class Math
    {
        public static float ZeroSign(float x)
        {
            if (x > float.Epsilon)
            {
                return 1f;
            }
            else if (x < -float.Epsilon)
            {
                return -1f;
            }
            return 0;
        }

    }
}
