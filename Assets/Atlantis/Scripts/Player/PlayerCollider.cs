using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Tools;

[RequireComponent(typeof(Collider))]
public class PlayerCollider : MonoBehaviour
{
    [SerializeField] float _minRepulseSpeed = 3f;
    [SerializeField] float _correctorCoef = 0.3f;
    [SerializeField] float _angularCoef = 0.5f;

    ContactPoint2D[] _buffer = new ContactPoint2D[50];

    Collider _collider;
    List<Collider2D> _contacts;

    public class Comparer : IComparer<ContactPoint2D>
    {
        public int Compare(ContactPoint2D x, ContactPoint2D y)
        {
            return y.separation.CompareTo(x.separation);
        }
    }

    Comparer _comparer = new Comparer();

    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    //void OnCollisionStay(Collision collision)
    //{

    //}

    //void FixedUpdate()
    //{
    //    int count = _collider.Get GetContacts(_buffer);

    //    Array.Sort(_buffer, 0, count, _comparer);

    //    for (int i = 0; i < count; i++)
    //    {
    //        GameObject go = _buffer[i].collider.gameObject;

    //        if(go.layer == LayerType.instance.decors)
    //        {
    //            Repulse(_buffer[i]);
    //        }
    //    }
    //}

    void Repulse(ContactPoint2D point)
    {
        GameObject origin = point.collider.gameObject;
        Vector2 currentVelocity = PlayerShip.instance.velocity;

        Vector2 velocity;

        if (currentVelocity.magnitude < 0.1f)
        {
            velocity = point.normal;
        }
        else
        {
            velocity = Vector2.Reflect(currentVelocity, point.normal);
        }

        float angular = Vector2.SignedAngle(currentVelocity, velocity) * _angularCoef;

        velocity = MathHelper.RotateVector(velocity, -angular * Mathf.Deg2Rad * _correctorCoef);

        if(velocity.magnitude < _minRepulseSpeed)
        {
            velocity.Normalize();
            velocity *= _minRepulseSpeed;
        }

        PlayerShip.instance.ApplyRepulseForce(velocity, angular, origin);
    }
}
