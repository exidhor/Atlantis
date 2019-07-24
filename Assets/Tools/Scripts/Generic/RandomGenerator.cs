using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class RandomGenerator : System.Random
    {
        public RandomGenerator(int seed)
            : base(seed)
        { }

        public bool NextBool()
        {
            return NextDouble() > 0.5f;
        }

        /// <summary>
        /// return an int between min [inclusive] and max [inclusive] 
        /// with the AnimationCurve as distribution
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="curve"></param>
        /// <returns></returns>
        public int NextDistributedInt(int min, int max, AnimationCurve curve)
        {
            float t = (float)NextDouble();
            float e = curve.Evaluate(t);
            e = Mathf.Clamp01(e);
            return min + (int)(e * (max - min));
        }

        /// <summary>
        /// return an int between 0 [inclusive] and max [inclusive] 
        /// with the AnimationCurve as distribution
        /// </summary>
        public int NextDistributedInt(int max, AnimationCurve curve)
        {
            return NextDistributedInt(0, max, curve);
        }

        /// <summary>
        /// return a float between min [inclusive] and max [inclusive] 
        /// with the AnimationCurve as distribution
        /// </summary>
        public float NextDistributedFloat(float min, float max, AnimationCurve curve)
        {
            float t = (float)NextDouble();
            float e = curve.Evaluate(t);
            e = Mathf.Clamp01(e);
            return min + (e * (max - min));
        }

        public float NextFloat(float min, float max)
        {
            float t = (float)NextDouble();
            return min + (t * (max - min));
        }

        public float NextFloat(Vector2 range)
        {
            return NextFloat(range.x, range.y);
        }

        public int Next(Vector2i range)
        {
            return Next(range.x, range.y);
        }

        public static int GetConstantRandom(int seed, int count, int precision = 10000000)
        {
            if (seed <= 0)
                seed = int.MaxValue;

            float a = Mathf.Sqrt(seed) / precision;
            float b = Mathf.Sin(1 / a);
            int c = (int)(Mathf.Floor(b * 1000) % count);

            return Mathf.Abs(c);
        }
    }
}

