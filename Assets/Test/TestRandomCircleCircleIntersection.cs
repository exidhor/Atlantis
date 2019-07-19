using UnityEngine;
using System.Collections.Generic;
using Tools;

[ExecuteInEditMode]
public class TestRandomCircleCircleIntersection : MonoBehaviour
{
    public Transform A;
    public Transform B;

    public float radiusA;
    public float radiusB;

    public bool spawn;
    public bool clear;
    public bool spawnPointOnSegment;

    CircleCircleIntersection inter;

    List<Vector2> _points = new List<Vector2>();

    void Update()
    {
        inter = MathHelper.CircleCircleIntersects(A.position, radiusA, B.position, radiusB);

        if(spawn)
        {
            Vector2 point = RandomHelper.PointInCircleCircleIntersection(A.position, radiusA, B.position, radiusB, inter);

            _points.Add(point);

            spawn = false;
        }

        if(clear)
        {
            _points.Clear();

            clear = false;
        }

        if(spawnPointOnSegment)
        {
            _points.Add(RandomHelper.NextBinomialPointOnSegment(A.position, B.position));

            spawnPointOnSegment = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(A.position, radiusA);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(B.position, radiusB);

        Gizmos.color = Color.black;
        for(int i = 0; i < _points.Count; i++)
        {
            Gizmos.DrawSphere(_points[i], 0.1f);
        }
    }
}
