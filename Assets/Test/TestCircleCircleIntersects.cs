using UnityEngine;
using System.Collections;
using Tools;

[ExecuteInEditMode]
public class TestCircleCircleIntersects : MonoBehaviour
{
    public Transform A;
    public Transform B;

    public float radiusA;
    public float radiusB;

    public CircleCircleIntersection inter;

    void Update()
    {
        inter = MathHelper.CircleCircleIntersects(A.position, radiusA, B.position, radiusB);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(A.position, radiusA);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(B.position, radiusB);

        if(inter.isValid)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(inter.left, 0.1f);

            Gizmos.color = Color.black;
            Gizmos.DrawSphere(inter.middle, 0.1f);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(inter.right, 0.1f);
        }
    }
}
