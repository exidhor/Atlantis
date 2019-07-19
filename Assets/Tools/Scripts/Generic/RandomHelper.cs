using UnityEngine;
using System.Collections;

namespace Tools
{
    public static class RandomHelper
    {
        public static float NextBinomialFloat(float min, float max)
        {
            return Random.Range(min, max) - Random.Range(min, max);
        }

        public static Vector2 PointInCircle(Vector2 center, float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            Vector2 direction = MathHelper.GetDirectionFromAngle(angle);
            float distance = Random.Range(0f, radius);

            return center + direction * distance;
        }

        public static Vector2 PointInCircleCircleIntersection(Vector2 circleA,
                                                              float radiusA,
                                                              Vector2 circleB,
                                                              float radiusB,
                                                              CircleCircleIntersection intersection)
        {
            bool shootOnA = Random.Range(0f, radiusA + radiusB) < radiusA;

            Vector2 center;
            float radius;

            if(shootOnA)
            {
                center = circleA;
                radius = radiusA;
            }
            else
            {
                center = circleB;
                radius = radiusB;
            }

            Vector2 o = NextBinomialPointOnSegment(circleA, circleB);
            Vector2 ov = o - center;

            float angle = MathHelper.Angle(ov);

            Vector2 c = MathHelper.GetPointOnCircle(center, radius, angle);

            Vector2 point = NextPointOnSegment(o, c);

            return point;
        }

        public static Vector2 NextBinomialPointOnSegment(Vector2 a, Vector2 b)
        {
            float p = NextBinomialFloat(0, 1);

            return PointOnSegment(a, b, p);
        }

        public static Vector2 NextPointOnSegment(Vector2 a, Vector2 b)
        {
            return PointOnSegment(a, b, Random.value);
        }

        static Vector2 PointOnSegment(Vector2 a, Vector2 b, float p)
        {
            Vector2 point = a + (a - b) * p;

            return point;
        }
    }
}
