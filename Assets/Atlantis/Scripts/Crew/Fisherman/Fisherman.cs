using UnityEngine;
using System.Collections;
using Tools;

public class Fisherman : Crew
{
    [Header("Logic")]
    [SerializeField] float _maxSpeedToFish;
    [SerializeField] float _fishTime;
    [SerializeField] float _extendCoef = 1.5f;
    [SerializeField] float _minLineLength = 2f;

    [Header("Linking")]
    [SerializeField] FishingLine _fishingLine;

    CircleCollider2D _collider;
    FishZone _fishZone;
    bool _isFishing;
    float _currentFishTime;

    Vector3 _floatPosition;

    bool canFish
    {
        get 
        {
            bool speedOK = (PlayerShip.instance.velocity.magnitude < _maxSpeedToFish);
            bool inputOK = !PlayerShip.instance.disableInputs;

            if(!speedOK)
            {
                Debug.Log("Speed not ok to fish (velocity : " + PlayerShip.instance.velocity + ")");
            }

            if (!inputOK)
            {
                Debug.Log("Input not ok to fish");
            }

            return speedOK && inputOK;
        }
    }

    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        _fishingLine.StopFishing();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerType.instance.layerFish)
        {
            _fishZone = other.gameObject.GetComponent<FishZone>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_fishZone != null
            && _fishZone.gameObject == other.gameObject)
        {
            _fishZone = null;
            StopFishing();
        }
    }

    void Update()
    {
        if(_fishZone != null)
        {
            Fish(Time.deltaTime);
        }
    }

    void Fish(float dt)
    {
        if(!_isFishing)
        {
            if(canFish)
            {
                StartFishing();
            }
        }
        else
        {
            if(!canFish)
            {
                Debug.Log("There is not anymore the condition to fish -- STOP --");
                StopFishing();
                return;
            }

            float lineDistance = Vector2.Distance(_floatPosition, transform.position);

            if (_collider.radius * _extendCoef < lineDistance)
            {
                Debug.Log("Not at range to fish -- STOP --");
                StopFishing();
                return;
            }

            _currentFishTime += dt;

            if (_currentFishTime > _fishTime)
            {
                _currentFishTime -= _fishTime;
                Cargo.instance.AddFish(1);
            }
        }
    }

    void StartFishing()
    {
        _isFishing = true;
        _currentFishTime = 0f;

        Vector3 target = _fishZone.transform.position;
        Vector2 from = transform.position;
        Vector2 point;

        float distToTarget = ((Vector2)target - from).sqrMagnitude;
        float distToPoint = _collider.radius;

        if (distToTarget < distToPoint)
        {
            if (distToTarget < _minLineLength * _minLineLength)
            {
                point = MathHelper.ShootPoint(from, target, _minLineLength);
            }
            else
            {
                point = target;
            }
        }
        else
        {
            float dist = Random.Range(_minLineLength, _collider.radius / 2f);
            point = MathHelper.ShootPoint(from, target, dist);
        }

        _floatPosition = new Vector3(point.x, point.y, target.z);
        _fishingLine.Land(_floatPosition);
    }

    void StopFishing()
    {
        _isFishing = false;
        _fishZone = null;
        _fishingLine.StopFishing();
    }
}
