using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class CircleIndicator : MonoBehaviour
{
    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;

    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
    }

    void RefreshCircle()
    {
        Vector3 pos = transform.position;

        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 2f);
        LineDrawer.positionCount = Size;
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float z = radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, pos + new Vector3(x, 0, z));
        }
    }

    void Update()
    {
        RefreshCircle();
    }
}
