using UnityEngine;
using System.Collections.Generic;
using Tools;

[RequireComponent(typeof(QTCircleCollider))]
public class HarborHandler : MonoBehaviour
{
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

    [Header("Harbor Handler")] 
    [SerializeField] HarborWindow _window;

    QTCircleCollider _collider;
    Harbor _harbor;

    bool _isAtRange = false;

    void Update()
    {
        float distance = HandleHarborDetection();

        if (distance >= 0f)
        {
            ActualizeHarborState(distance);
        }
    }

    void ActualizeHarborState(float distance)
    {
        if (_harbor != null)
        {
            float maxDistance = collider.radius + _harbor.innerRadius;

            if(_isAtRange && maxDistance < distance)
            {
                _isAtRange = false;
                _harbor.SetIndicatorState(false);
                _window.Disappear();
            }
            else if(!_isAtRange && maxDistance > distance)
            {
                _isAtRange = true;
                _harbor.SetHarborWindow(_window);
                _harbor.SetIndicatorState(true);
                _window.Appear();
            }
        }
    }

    float HandleHarborDetection()
    {
        if (_harbor != null)
        {
            float distance = Vector2.Distance(_harbor.center,
                                              collider.center);

            if (distance > _harbor.radius + collider.radius)
            {
                _harbor.SetIndicatorVisibility(false);
                _harbor = null;

                return -1;
            }

            return distance;
        }
        else
        {
            List<QTCircleCollider> found = QuadTreeCircleManager.instance.Retrieve(collider);

            if (found.Count > 0)
            {
                _harbor = (Harbor)found[0];
                _harbor.SetIndicatorVisibility(true);
                _isAtRange = false;

                return Vector2.Distance(_harbor.center,
                                        collider.center);
            }

            return -1;
        }
    }

    public void AskForDeal()
    {
        if (_harbor == null || !_isAtRange)
            return;

        bool done = _harbor.AskForDeal();

        if(done)
        {
            _window.Disappear();
            _harbor.Close();
            _harbor = null;
        }
    }
}
