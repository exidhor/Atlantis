using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class FishZone : MonoBehaviour
{
    public float radius
    {
        get
        {
            return collider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
        }
    }

    SphereCollider collider
    {
        get
        {
            if(_collider == null)
            {
                _collider = GetComponent<SphereCollider>();
            }

            return _collider;
        }
    }

    public FishType fishType
    {
        get { return _fishType; }
    }

    [SerializeField] FishType _fishType;

    SphereCollider _collider;
}
