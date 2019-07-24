using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class TestLineIntersectRect : MonoBehaviour
{
    public Transform P1;
    public Transform P2;

    public Transform R0;
    public Transform R1;

    Rect _rect;
    Vector2 _intersection;
    bool _collision;

    void Update()
    {
        _rect = MathHelper.ConstructRect(R0.position, R1.position);

        _collision = MathHelper.LineIntersectRect(out _intersection, P1.position, P2.position, _rect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _collision ? Color.red : Color.green;
        Gizmos.DrawWireCube(_rect.center, _rect.size);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(P1.position, P2.position);

        float radius = 0.2f;

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(P1.position, radius);
        Gizmos.DrawWireSphere(P2.position, radius);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(R0.position, radius);
        Gizmos.DrawWireSphere(R1.position, radius);

        if(_collision)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_intersection, radius);
        }
    }
}
