﻿using UnityEngine;
using System.Collections;

namespace Tools
{
    public static class WorldConversion
    {
        public static Vector2 ToVector2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        public static Vector3 ToVector3(Vector2 v2)
        {
            return new Vector3(v2.x, 0f, v2.y);
        }
    }
}
