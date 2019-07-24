using System;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public struct Vector2i
    {
        public int x;
        public int y;

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2i zero = new Vector2i(0, 0);
        public static Vector2i one = new Vector2i(1, 1);
        public static Vector2i left = new Vector2i(-1, 0);
        public static Vector2i right = new Vector2i(1, 0);
        public static Vector2i bottom = new Vector2i(0, -1);
        public static Vector2i top = new Vector2i(0, 1);

        public static Vector2i operator +(Vector2i c1, Vector2i c2)
        {
            return new Vector2i(c1.x + c2.x, c1.y + c2.y);
        }

        public static Vector2i operator -(Vector2i c1, Vector2i c2)
        {
            return new Vector2i(c1.x - c2.x, c1.y - c2.y);
        }

        public static Vector2i operator -(Vector2i c1)
        {
            return new Vector2i(-c1.x, -c1.y);
        }
        
        public static Vector2i operator *(Vector2i c, int i)
        {
            return new Vector2i(c.x * i, c.y * i);
        }

        public static Vector2 operator *(Vector2i c, float f)
        {
            return new Vector2(c.x * f, c.y * f);
        }

        public static Vector2 operator *(float f, Vector2i c)
        {
            return new Vector2(c.x * f, c.y * f);
        }

        public static Vector2 operator /(Vector2i c, int i)
        {
            return new Vector2(c.x, c.y) / i;
        }

        public static Vector2 operator /(Vector2i c, float f)
        {
            return new Vector2(c.x, c.y) / f;
        }

        public static bool operator ==(Vector2i c1, Vector2i c2)
        {
            return c1.x == c2.x
                   && c1.y == c2.y;
        }

        public static bool operator !=(Vector2i c1, Vector2i c2)
        {
            return !(c1 == c2);
        }

        public static implicit operator Vector2(Vector2i c)
        {
            return new Vector2(c.x, c.y);
        }

        public static explicit operator Vector2i(Vector2 c)
        {
            return new Vector2i((int)c.x, (int)c.y);
        }

        public static int ManhattanDistance(Vector2i a, Vector2i b)
        {
            Vector2i d = a - b;
            return Mathf.Abs(d.x) + Mathf.Abs(d.x);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Vector2i?;

            if(item.HasValue)
            {
                return x == item.Value.x 
                    && y == item.Value.y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }

        public override string ToString()
        {
            return "Vector2i(" + x + ", " + y + ")";
        }

        public static int SizeOf()
        {
            return sizeof(int) * 2;
        }

        public int CompareTo(Vector2i v2i)
        {
            int dx = x - v2i.x;
            if (dx != 0) return dx;

            return y - v2i.y;
        }
    }
}