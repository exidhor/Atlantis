using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class FishZone : MonoBehaviour
{
    public float radius
    {
        get
        {
            return collider.radius;
        }
    }

    CircleCollider2D collider
    {
        get
        {
            if(_collider == null)
            {
                _collider = GetComponent<CircleCollider2D>();
            }

            return _collider;
        }
    }

    CircleCollider2D _collider;
}
