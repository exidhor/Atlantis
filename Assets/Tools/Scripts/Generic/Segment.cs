using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tools
{
    public struct Segment
    {
        public Vector3 A;
        public Vector3 B;

        public Vector2 negativeDelta
        {
            get { return new Vector2(A.x - B.x, A.y - B.y); }
        }

        public Vector2 delta
        {
            get { return new Vector2(B.x - A.x, B.y - A.y); }
        }

        public Segment(Segment segment)
        {
            A = segment.A;
            B = segment.B;
        }

        public Segment(Vector3 a, Vector3 b)
        {
            A = a;
            B = b;
        }

        public float Length()
        {
            float xDeltaBuff = delta.x;
            float yDeltaBuff = delta.y;

            return Mathf.Sqrt(xDeltaBuff * xDeltaBuff + yDeltaBuff * yDeltaBuff);
        }

        public Vector2 Direction()
        {
            return B - A;
        }
    }
}