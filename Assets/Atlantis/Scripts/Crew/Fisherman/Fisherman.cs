using UnityEngine;
using System.Collections.Generic;
using Tools;

public class Fisherman : Crew
{
    public override CrewType type 
    { 
        get { return CrewType.Fisherman; } 
    }

    [Header("Logic")]
    [SerializeField] float _maxSpeedToFish;
    [SerializeField] float _fishTime;
    [SerializeField] float _extendCoef = 1.5f;
    [SerializeField] float _minLineLength = 2f;
    [SerializeField] bool _restartAfterCatching;

    [Header("Linking")]
    [SerializeField] FishingLine _fishingLine;

    QTCircleCollider _collider;
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

            //if(!speedOK)
            //{
            //    Debug.Log("Speed not ok to fish (velocity : " + PlayerShip.instance.velocity + ")");
            //}

            //if (!inputOK)
            //{
            //    Debug.Log("Input not ok to fish");
            //}

            return speedOK && inputOK;
        }
    }

    void Awake()
    {
        _collider = GetComponent<QTCircleCollider>();
    }

    void OnEnable()
    {
        _fishingLine.StopFishing();
    }

    // todo : opti this
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerType.instance.layerFish)
    //    {
    //        _fishZone = other.gameObject.GetComponent<FishZone>();
    //    }
    //}

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
        else
        {
            List<QTCircleCollider> found = QuadTreeCircleManager.instance.Retrieve(_collider);

            QTCircleCollider best = null;
            float bestDistance = float.MaxValue;
            for(int i = 0; i < found.Count; i++)
            {
                float distance = Vector2.Distance(found[i].center, _collider.center);

                if(distance < bestDistance)
                {
                    best = found[i];
                    bestDistance = distance;
                }
            }

            if(best != null)
            {
                _fishZone = (FishZone)best;
            }
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
            if(canFish && Cargo.instance.CanStore(_fishZone.fishType))
            {
                StartFishing();
            }
        }
        else
        {
            if(!canFish || !Cargo.instance.CanStore(_fishZone.fishType))
            {
                // Debug.Log("There is not anymore the condition to fish -- STOP --");
                StopFishing();
                return;
            }

            _currentFishTime += dt;

            if (_currentFishTime > _fishTime)
            {
                _currentFishTime -= _fishTime;
                Cargo.instance.AddFish(_fishZone.fishType, 1);

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

        //_floatPosition = new Vector3(shootPoint.x, target.y, shootPoint.z);
        _floatPosition = new Vector3(shootPoint.x, 0f, shootPoint.z);
        _fishingLine.Land(_floatPosition);
    }

    Vector2 ShootLine()
    {
        Vector2 centerA = _fishZone.center;
        float radiusA = _fishZone.radius;

        Vector2 centerB = _collider.center;
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
        _fishingLine.StopFishing();
    }
}
