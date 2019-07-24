using UnityEngine;
using System.Collections;
using Tools;

[RequireComponent(typeof(QTCircleCollider))]
public class FishZone : MonoBehaviour
{
    public float radius
    {
        get
        {
            return collider.radius;
        }
    }

    QTCircleCollider collider
    {
        get
        {
            if(_collider == null)
            {
                _collider = GetComponent<QTCircleCollider>();
            }

            return _collider;
        }
    }

    public FishType fishType
    {
        get { return _fishType; }
    }

    [SerializeField] FishType _fishType;

    QTCircleCollider _collider;
}
