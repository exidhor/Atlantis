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
    [SerializeField] bool _restartAfterCatching;

    [Header("Linking")]
    [SerializeField] FishingLine _fishingLine;

    SphereCollider _collider;
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
                //Debug.Log("Speed not ok to fish (velocity : " + PlayerShip.instance.velocity + ")");
            }

            if (!inputOK)
            {
                //Debug.Log("Input not ok to fish");
            }

            return speedOK && inputOK;
        }
    }

    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    void OnEnable()
    {
        _fishingLine.StopFishing();
    }

    // todo : opti this
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerType.instance.layerFish)
        {
            _fishZone = other.gameObject.GetComponent<FishZone>();
        }
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (_fishZone != null
    //        && _fishZone.gameObject == other.gameObject)
    //    {
    //        _fishZone = null;
    //        StopFishing();
    //    }
    //}

    void Update()
    {
        if(_fishZone != null)
        {
            Fish(Time.deltaTime);
        }
        else if(_isFishing)
        {
            StopFishing();
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;

        _fishingLine.Actualize(Time.deltaTime);
    }

    void Fish(float dt)
    {
        if(_fishZone != null
            && _fishZone.radius + (_collider.radius * _extendCoef) 
                < Vector2.Distance(WorldConversion.ToVector2(transform.position), 
                                   WorldConversion.ToVector2(_fishZone.transform.position)))
        {
            _fishZone = null;
        }
        else if (!_isFishing)
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

            _currentFishTime += dt;

            if (_currentFishTime > _fishTime)
            {
                _currentFishTime -= _fishTime;
                Cargo.instance.AddFish(1, _fishZone.fishType);

                if(_restartAfterCatching)
                    StopFishing();
            }
        }
    }

    void StartFishing()
    {
        if (_fishingLine.isStopping) return;

        _isFishing = true;
        _currentFishTime = 0f;

        Vector3 shootPoint = WorldConversion.ToVector3(ShootLine());

        Vector3 target = _fishZone.transform.position;

        _floatPosition = new Vector3(shootPoint.x, target.y, shootPoint.z);
        _fishingLine.Land(_floatPosition);
    }

    Vector2 ShootLine()
    {
        Vector2 centerA = WorldConversion.ToVector2(_fishZone.transform.position);
        float radiusA = _fishZone.radius;

        Vector2 centerB = WorldConversion.ToVector2(transform.position);
        float radiusB = _collider.radius;

        CircleCircleIntersection intersect = MathHelper.CircleCircleIntersects(centerA,
                                                                               radiusA,
                                                                               centerB,
                                                                               radiusB);

        if (!intersect.isValid)
        {
            Vector2 center;
            float radius;

            if (radiusA > radiusB)
            {
                center = centerB;
                radius = radiusB;
            }
            else
            {
                center = centerA;
                radius = radiusA;
            }

            return RandomHelper.PointInCircle(center, radius);
        }

        return RandomHelper.PointInCircleCircleIntersection(centerA,
                                                            radiusA,
                                                            centerB,
                                                            radiusB,
                                                            intersect);
    }

    void StopFishing()
    {
        if(_fishingLine.isLanding)
        {
            return;
        }

        _isFishing = false;
        //_fishZone = null;
        _fishingLine.StopFishing();
    }
}
