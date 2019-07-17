using UnityEngine;
using System.Collections.Generic;
using Tools;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollider : MonoBehaviour
{
    [SerializeField] float _minRepulseSpeed = 3f;
    [SerializeField] float _correctorCoef = 0.3f;

    ContactPoint2D[] _buffer = new ContactPoint2D[50];

    Collider2D _collider;
    List<Collider2D> _contacts;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        int count = _collider.GetContacts(_buffer);

        for(int i = 0; i < count; i++)
        {
            GameObject go = _buffer[i].collider.gameObject;

            if(go.layer == LayerType.instance.decors)
            {
                Repulse(_buffer[i]);
            }
        }
    }

    void Repulse(ContactPoint2D point)
    {
        GameObject origin = point.collider.gameObject;
        Vector2 currentVelocity = PlayerShip.instance.velocity;
        Vector2 velocity = Vector2.Reflect(currentVelocity, point.normal);
        float angular = Vector2.SignedAngle(currentVelocity, velocity);

        velocity = MathHelper.RotateVector(velocity, -angular * Mathf.Deg2Rad * _correctorCoef);

        if(velocity.magnitude < _minRepulseSpeed)
        {
            velocity.Normalize();
            velocity *= _minRepulseSpeed;
        }

        PlayerShip.instance.ApplyRepulseForce(velocity, angular, origin);
    }
}
