using UnityEngine;
using System.Collections;
using MemoryManagement;
using Tools;

public abstract class Bullet : UnityPoolObject
{
    public abstract BulletType type { get; }

    protected Vector3 start
    {
        get { return _startingPos; }
    }

    protected Vector3 end
    {
        get { return _target.transform.position; }
    }

    protected float duration
    {
        get { return _duration; }
    }

    Vector3 _startingPos;
    ITargetable _target;

    int _damage;
    float _duration;
    float _currentTime;

    protected abstract void Move(float dt);

    public void Init(Vector3 startingPos,
                     ITargetable target, 
                     int damage,
                     float speed)
    {
        transform.position = startingPos;
        _startingPos = startingPos;

        _target = target;

        _damage = damage;
        _duration = ComputeDuration(speed);

        _currentTime = 0f;
    }

    float ComputeDuration(float speed)
    {
        float distance = Vector2.Distance(WorldConversion.ToVector2(transform.position),
                                          WorldConversion.ToVector2(_target.transform.position));

        return speed / distance;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        _currentTime += dt;

        if(_currentTime >= _duration)
        {
            _target.DealDamage(_damage);
            Release();
        }
        else
        {
            Move(dt);
        }
    }
}
