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

    //[Header("Harbor Handler")] 
    //[SerializeField] HarborWindowManager _window;

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

        if(_harbor != null && _isAtRange)
        {
            if(!_harbor.isOpen)
            {
                HarborWindowManager.instance.ActualizeCloseState(_harbor.amountTimeLeft01);
                
            }
            else
            {
                if (HarborWindowManager.instance.CheckOpenState())
                {
                    SetInfo(_harbor);

                    HarborWindowManager.instance.SetIsOpen(_harbor.isOpen);
                }
            }
        }
    }

    void SetInfo(Harbor harbor)
    {
        if (_harbor.isOpen)
        {
            HarborWindowManager.instance.SetOpenInfo(_harbor);
        }
        else
        {
            HarborWindowManager.instance.SetCloseInfo(_harbor);
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
                HarborWindowManager.instance.Disappear();
            }
            else if(!_isAtRange && maxDistance > distance)
            {
                _isAtRange = true;
                HarborWindowManager.instance.Appear(HarborType.Fish);
                SetInfo(_harbor);
                _harbor.SetIndicatorState(true);
                HarborWindowManager.instance.SetIsOpen(_harbor.isOpen);
                //HarborWindowManager.instance.Appear(HarborType.Fish);
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
            //_window.SetCloseState(FishLibrary.instance.genericFishIcon);
            _harbor.Close();
            HarborWindowManager.instance.SetCloseInfo(_harbor);
        }
        else
        {
            HarborWindowManager.instance.OnRejectDeal();
        }
    }
}
