using UnityEngine;
using System.Collections.Generic;
using Tools;

public abstract class CrewWithRange<T> : Crew
    where T : QTCircleCollider
{
    protected abstract float extendCoef { get; }
    protected abstract float actionDuration { get; }

    T _zone;
    QTCircleCollider _collider;

    float _actionTime;
    bool _isInAction;

    protected abstract bool CanStartAction();
    protected abstract void OnStartAction();

    protected abstract bool CanStopAction();
    protected abstract void OnStopAction();

    protected abstract bool CanDoAction();
    protected abstract void OnActionComplete();

    void Awake()
    {
        _collider = GetComponent<QTCircleCollider>();
    }

    void Update()
    {
        if(_zone != null)
        {
            if(!IsZoneInRange())
            {
                _zone = null;
            }
            else if(!_isInAction)
            {
                if(CanStartAction())
                {
                    StartAction();
                }
            }
            else
            {
                if(IsZoneEnable())
                {
                    DoAction(Time.deltaTime);
                }
                else
                {
                    StopAction();
                    _zone = null;
                }
            }
        }
        else if(_isInAction)
        {
            StopAction();
        }
        else
        {
            FindZone();
        }
    }

    void DoAction(float dt)
    {
        if(CanDoAction())
        {
            StopAction();
        }
        else
        {
            _actionTime += dt;

            if(_actionTime > actionDuration)
            {
                _actionTime -= actionDuration;
                OnActionComplete();
            }
        }
    }

    void StartAction()
    {
        _isInAction = true;
        _actionTime = 0f;

        OnStartAction();
    }

    void StopAction()
    {
        if (!CanStopAction()) return;

        _isInAction = false;
        _actionTime = 0f;

        OnStopAction();
    }

    bool IsZoneInRange()
    {
        float maxDist = _zone.radius + (_collider.radius * extendCoef);
        float dist = Vector2.Distance(WorldConversion.ToVector2(transform.position),
                                      WorldConversion.ToVector2(_zone.transform.position));

        return (maxDist > dist);
    }

    bool IsZoneEnable()
    {
        return _zone.isEnable;
    }

    void FindZone()
    {
        List<QTCircleCollider> found = QuadTreeCircleManager.instance.Retrieve(_collider);

        QTCircleCollider best = null;
        float bestDistance = float.MaxValue;
        for (int i = 0; i < found.Count; i++)
        {
            float distance = Vector2.Distance(found[i].center, _collider.center);

            if (distance < bestDistance)
            {
                best = found[i];
                bestDistance = distance;
            }
        }

        if (best != null)
        {
            _zone = (T)best;
        }
    }
}
