using UnityEngine;
using System;

namespace Tools
{
    [Serializable]
    public struct CircleCircleIntersection
    {
        public static CircleCircleIntersection invalid = new CircleCircleIntersection(false, Vector2.zero, Vector2.zero);

        public bool isValid;
        public Vector2 left;
        public Vector2 right;

        public CircleCircleIntersection(bool isValid, Vector2 left, Vector2 right)
        {
            this.isValid = isValid;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// Gather all the usefull methods around globalbounds
    /// </summary>
    public static class MathHelper
    {
        public static readonly float TwoSqrt = Mathf.Sqrt(2);

        /// <summary>
        /// Construct the movement from the direction and speed.
        /// We also check if the offset is long enough (greater than min)
        ///  to be relevant
        /// </summary>
        /// <param name="offsetWithTarget">The difference between target and source</param>
        /// <param name="speed">The final speed of the movement</param>
        /// <param name="min">the mininum distance to make relevant the offset</param>
        /// <returns>The movement</returns>
        public static Vector2 ConstructMovement(Vector2 offsetWithTarget, float speed, float min)
        {
            if (offsetWithTarget.sqrMagnitude > min)
            {
                offsetWithTarget.Normalize();
                offsetWithTarget *= speed;
            }
            else
            {
                offsetWithTarget = Vector2.zero;
            }

            return offsetWithTarget;
        }


        /// <summary>
        /// Rotate a vector of x radian.
        /// </summary>
        /// <param name="vector2">The vector to rotate</param>
        /// <param name="angleInRadian">The angle in radiant</param>
        /// <returns>The rotated vector</returns>
        /// <source>http://stackoverflow.com/questions/4780119/2d-euclidean-vector-rotations</source>
        public static Vector2 RotateVector(Vector2 vector2, float angleInRadian)
        {
            float cos = Mathf.Cos(angleInRadian);
            float sin = Mathf.Sin(angleInRadian);

            Vector2 result = new Vector2();

            result.x = vector2.x * cos - vector2.y * sin;
            result.y = vector2.x * sin + vector2.y * cos;

            return result;
        }

        /// <summary>
        /// Return a normalized direction vector from the given angle in radian
        /// </summary>
        /// <param name="angleInRadian">The given angle</param>
        /// <returns>The normalized vector</returns>
        /// <source>http://math.stackexchange.com/questions/180874/convert-angle-radians-to-a-heading-vector</source>
        public static Vector2 GetDirectionFromAngle(float angleInRadian)
        {
            return new Vector2(Mathf.Cos(angleInRadian),
                Mathf.Sin(angleInRadian));
        }

        /// <summary>
        /// Return an angle between -90 and 90 degrees representing the smallest difference between the two vector
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <source>http://answers.unity3d.com/questions/24983/how-to-calculate-the-angle-between-two-vectors.html</source>
        public static float Angle(Vector2 from, Vector2 to)
        {
            return Mathf.DeltaAngle(Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg,
                                    Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg);
        }

        /// <summary>
        /// Return an angle in radiant
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <source>http://answers.unity3d.com/questions/24983/how-to-calculate-the-angle-between-two-vectors.html</source>
        public static float Angle(Vector2 vector2)
        {
            return Mathf.Atan2(vector2.y, vector2.x);
        }

        /// <summary>
        /// Wraps a value around some significant range.
        ///
        /// Similar to modulo, but works in a unary direction over any range (including negative values).
        ///
        /// ex:
        /// Wrap(8,6,2) == 4
        /// Wrap(4,2,0) == 0
        /// Wrap(4,2,-2) == -2
        /// </summary>
        /// <param name="value">value to wrap</param>
        /// <param name="max">max in range</param>
        /// <param name="min">min in range</param>
        /// <returns>A value wrapped around min to max</returns>
        /// <remarks></remarks>
        public static float Wrap(float value, float max, float min)
        {
            value -= min;
            max -= min;
            if (max == 0)
                return min;

            value = value % max;
            value += min;
            while (value < min)
            {
                value += max;
            }

            return value;

        }

        /// <summary>
        /// set an angle with in the bounds of -PI to PI
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static float NormalizeAngle(float angle, bool useRadians)
        {
            float rd = (useRadians ? Mathf.PI : 180);
            return Wrap(angle, rd, -rd);
        }

        /// <summary>
        /// Shoot a point on a segment (same trajectory)
        /// </summary>
        /// <param name="origin">origin this point will determine the sens of the projection it will be the closer point of the new shooted point)</param>
        /// <param name="direction">direction this point determine the direction (it allowed to get a segment)</param>
        /// <param name="distance">distance the distance between the origin and the new shooted point</param>
        /// <returns>the position of the new shooted point</returns>
        public static Vector2 ShootPoint(Vector2 origin, Vector2 direction, float distance)
        {
            float segmentLength = Vector2.Distance(origin, direction);

            // place the point at the width distance of the end of the edge
            float deltaX = distance * (direction.x - origin.x) / segmentLength;
            float deltaY = distance * (direction.y - origin.y) / segmentLength;

            float newX = origin.x + deltaX;
            float newY = origin.y + deltaY;

            return new Vector2(newX, newY);
        }

        /// <summary>
        /// Return a specific point on the circumference of a circle, from a given angle
        /// </summary>
        /// <param name="circleCenter">The center position of the circle</param>
        /// <param name="radius">The circle radius</param>
        /// <param name="angleInRadian">The angle in radian</param>
        /// <returns>The point on the circumference of the circle</returns>
        /// <source>http://gamedev.stackexchange.com/questions/18340/get-position-of-point-on-circumference-of-circle-given-an-angle</source>
        public static Vector2 GetPointOnCircle(Vector2 circleCenter, float radius, float angleInRadian)
        {
            Vector2 newPoint = Vector2.zero;

            newPoint.x = Mathf.Cos(angleInRadian) * radius + circleCenter.x;
            newPoint.y = Mathf.Sin(angleInRadian) * radius + circleCenter.y;

            return newPoint;
        }

        /// <summary>
        /// Compute int pow by a optimized way
        /// </summary>
        /// <param name="baseValue">The value which we want to compute</param>
        /// <param name="exp">The exponential value</param>
        /// <returns>The int result</returns>
        /// <source>http://stackoverflow.com/questions/101439/the-most-efficient-way-to-implement-an-integer-based-power-function-powint-int</source>
        public static int Pow(int baseValue, int exp)
        {
            int result = 1;

            while (exp != 0)
            {
                if ((exp & 1) != 0)
                    result *= baseValue;
                exp >>= 1;
                baseValue *= baseValue;
            }

            return result;
        }

        /// <summary>
        /// Compute the power of two from a given expo
        /// </summary>
        /// <param name="power">The expo</param>
        /// <returns>The result</returns>
        public static int PowerOfTwo(int power)
        {
            return Pow(2, power);
        }

        /// <summary>
        /// find the nearest point on the perimeter of the rectangle to the outside point
        /// </summary>
        /// <param name="rect">The rect</param>
        /// <param name="outsidePoint"></param>
        /// <returns></returns>
        /// <source>http://stackoverflow.com/questions/20453545/how-to-find-the-nearest-point-in-the-perimeter-of-a-rectangle-to-a-given-point</source>
        public static Vector2 ClosestPointToRect(Rect rect, Vector2 outsidePoint)
        {
            return ClosestPointToRect(rect.xMin, rect.xMax, rect.yMax, rect.yMin, outsidePoint);
        }

        public static Vector2 ClosestPointToRect(float left, float right, float top, float bottom, Vector2 outsidePoint)
        {
            outsidePoint.x = Mathf.Clamp(outsidePoint.x, left, right);
            outsidePoint.y = Mathf.Clamp(outsidePoint.y, bottom, top);

            float deltaLeft = Mathf.Abs(outsidePoint.x - left);
            float deltaRight = Mathf.Abs(outsidePoint.x - right);
            float deltaTop = Mathf.Abs(outsidePoint.y - top);
            float deltaBottom = Mathf.Abs(outsidePoint.y - bottom);

            float min = Mathf.Min(deltaLeft, deltaRight, deltaTop, deltaBottom);

            if (min == deltaTop)
                return new Vector2(outsidePoint.x, top);

            if (min == deltaBottom)
                return new Vector2(outsidePoint.x, bottom);

            if (min == deltaLeft)
                return new Vector2(left, outsidePoint.y);

            return new Vector2(right, outsidePoint.y);
        }

        /// <summary>
        /// find the nearest point on the perimeter of the rectangle to the outside point
        /// </summary>
        /// <param name="rect">The rect</param>
        /// <param name="outsidePoint"></param>
        /// <returns></returns>
        /// <source>http://stackoverflow.com/questions/20453545/how-to-find-the-nearest-point-in-the-perimeter-of-a-rectangle-to-a-given-point</source>
        public static Vector2 ClosestPointToBounds(Bounds bounds, Vector2 outsidePoint)
        {
            return ClosestPointToRect(bounds.min.x, bounds.max.x, bounds.max.y, bounds.min.y, outsidePoint);
        }

        /// <summary>
        /// Find the intersection between two circles.
        /// source : https://stackoverflow.com/questions/3349125/circle-circle-intersection-points
        /// </summary>
        /// <returns>The circle intersects.</returns>
        /// <param name="centerA">Center a.</param>
        /// <param name="centerB">Center b.</param>
        public static CircleCircleIntersection CircleCircleIntersects(Vector2 centerA, float radiusA, Vector2 centerB, float radiusB)
        {
            float d = Vector2.Distance(centerA, centerB);

            if (d > radiusA + radiusB)
                return CircleCircleIntersection.invalid;

            if (d < Mathf.Abs(radiusA - radiusB))
                return CircleCircleIntersection.invalid;

            if (d == 0)
                return CircleCircleIntersection.invalid;

            // a = (r02 - r12 + d2 ) / (2 d)
            float a = (radiusA * radiusA - radiusB * radiusB + d * d) / (2 * d);

            // P2 = P0 + a ( P1 - P0 ) / d
            Vector2 middle = centerA + a * (centerB - centerA) / d;

            // h2 = r02 - a2
            float h = Mathf.Sqrt(radiusA * radiusA - a * a);

            // x3 = x2 +- h ( y1 - y0 ) / d
            float m = h * (centerB.y - centerA.y) / d;

            float xLeft = middle.x + m;
            float xRight = middle.x - m;

            // y3 = y2 -+ h ( x1 - x0 ) / d

            float n = h * (centerB.x - centerA.x) / d;

            float yLeft = middle.y - n;
            float yRight = middle.y + n;

            return new CircleCircleIntersection(true,
                                                new Vector2(xLeft, yLeft),
                                                new Vector2(xRight, yRight));
        }

        public static Rect ConstructRect(Vector2 p0, Vector2 p1)
        {
            float minX, maxX;

            if (p0.x < p1.x)
            {
                minX = p0.x;
                maxX = p1.x;
            }
            else
            {
                minX = p1.x;
                maxX = p0.x;
            }

            float minY, maxY;

            if (p0.y < p1.y)
            {
                minY = p0.y;
                maxY = p1.y;
            }
            else
            {
                minY = p1.y;
                maxY = p0.y;
            }

            Vector2 min = new Vector2(minX, minY);
            Vector2 max = new Vector2(maxX, maxY);

            return new Rect(min, max - min);
        }

        public static bool LineIntersectRect(out Vector2 intersection, Vector2 origin, Vector2 endPoint, Rect rect)
        {
            Vector2 botLeft = new Vector2(rect.xMin, rect.yMin);
            Vector2 topLeft = new Vector2(rect.xMin, rect.yMax);
            Vector2 topRight = new Vector2(rect.xMax, rect.yMax);
            Vector2 botRight = new Vector2(rect.xMax, rect.yMin);

            Segment line = new Segment(origin, endPoint);

            Segment left = new Segment(botLeft, topLeft);
            Segment top = new Segment(topLeft, topRight);
            Segment right = new Segment(topRight, botRight);
            Segment bot = new Segment(botRight, botLeft);

            bool isInsideRect = rect.Contains(origin);

            bool toTheRight = (origin.x < endPoint.x) ^ isInsideRect;
            bool toTheTop = (origin.y < endPoint.y) ^ isInsideRect;

            if (toTheRight)
            {
                if (IntersectSegments(out intersection, line, left))
                {
                    return true;
                }
            }
            else
            {
                if (IntersectSegments(out intersection, line, right))
                {
                    return true;
                }
            }

            if (toTheTop)
            {
                if (IntersectSegments(out intersection, line, bot))
                {
                    return true;
                }

            }
            else
            {
                if (IntersectSegments(out intersection, line, top))
                {
                    return true;
                }
            }

            intersection = Vector2.zero;
            return false;
        }

        public static float Cross2D(Vector2 v, Vector2 w)
        {
            return v.x * w.y - v.y * w.x;
        }

        // source : https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect
        public static bool IntersectSegments(out Vector2 intersection, Segment first, Segment second)
        {
            Vector2 p = first.A;
            Vector2 r = first.delta;

            Vector2 q = second.A;
            Vector2 s = second.delta;

            Vector2 o = (q - p); // offset

            float RxS = Cross2D(r, s);
            float OxS = Cross2D(o, s);
            float OxR = Cross2D(o, r);

            if (RxS == 0f)
            {
                if (OxR == 0f)
                {
                    // two segments are colinear
                    float RdotR = Vector2.Dot(r, r);

                    // The crossing point of the second segment expressed in terms of the equation of the first segment
                    float t0 = Vector2.Dot(o, r) / RdotR;

                    // The pointLine point of the second segment expressed in terms of the equation of the first segment
                    float t1 = t0 + Vector2.Dot(s, r) / RdotR;

                    bool t0Intersects = (0 <= t0 && t0 <= 1);
                    bool t1Intersects = (0 <= t1 && t1 <= 1);

                    if (t0Intersects)
                    {
                        // they are overlapping
                        intersection = q;
                        return true;
                    }

                    if (t1Intersects)
                    {
                        // they are overlapping
                        intersection = s;
                        return true;
                    }
                    else
                    {
                        // they are disjoint
                        intersection = new Vector2();
                        return false;
                    }
                }
                else
                {
                    // line are parallel
                    intersection = new Vector2();
                    return false;
                }
            }

            float t = OxS / RxS;
            float u = OxR / RxS;

            if ((0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                intersection = p + t * r;
                return true;
            }
            else
            {
                // lines are not parallel but segments do not intersect
                intersection = new Vector2();
                return false;
            }
        }

        // source : https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines
        public static bool IntersectLines(out Vector2 intersection, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            // we want equations in form of : Ax + By = C
            float a0 = p1.y - p0.y;
            float b0 = p1.x - p0.x;
            float c0 = a0 * p0.x + b0 * p0.y;

            float a1 = p3.y - p2.y;
            float b1 = p3.x - p2.x;
            float c1 = a1 * p1.x + b1 * p1.y;

            float det = a0 * b1 - a1 * b0;

            if (-float.Epsilon < det && det < float.Epsilon)
            {
                // Line are parallel
                intersection = new Vector2();
                return false;
            }

            float x = (b1 * c0 - b0 * c1) / det;
            float y = (a0 * c1 - a1 * c0) / det;

            intersection = new Vector2(x, y);

            return true;
        }

        public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
            var mid = Vector3.Lerp(start, end, t);
            return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }

        public static Vector2 Parabola2D(Vector2 start, Vector2 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
            var mid = Vector2.Lerp(start, end, t);
            return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
        }
    }
}
