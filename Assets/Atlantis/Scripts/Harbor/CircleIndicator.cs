using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class CircleIndicator : MonoBehaviour
{
    public float radius
    {
        get { return _radius; }
    }

    [SerializeField] float _thetaScale = 0.01f;
    [SerializeField] float _radius = 3f;

    private int _size;
    private LineRenderer _lineDrawer;
    private float _theta = 0f;

    void Start()
    {
        _lineDrawer = GetComponent<LineRenderer>();
    }

    public void SetVisible(bool isVisible)
    {
        _lineDrawer.enabled = isVisible;
    }

    public void SetColor(Gradient color)
    {
        _lineDrawer.colorGradient = color;
    }

    void RefreshCircle()
    {
        Vector3 pos = transform.position;

        _theta = 0f;
        _size = (int)((1f / _thetaScale) + 2f);
        _lineDrawer.positionCount = _size;
        for (int i = 0; i < _size; i++)
        {
            _theta += (2.0f * Mathf.PI * _thetaScale);
            float x = _radius * Mathf.Cos(_theta);
            float z = _radius * Mathf.Sin(_theta);
            _lineDrawer.SetPosition(i, pos + new Vector3(x, 0, z));
        }
    }

    void Update()
    {
        RefreshCircle();
    }
}
