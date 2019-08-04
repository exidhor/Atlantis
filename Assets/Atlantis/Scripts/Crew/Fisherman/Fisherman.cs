using UnityEngine;
using System.Collections.Generic;
using Tools;

[RequireComponent(typeof(QTCircleCollider))]
public class Fisherman : CrewWithRange<FishZone>
{
    public override CrewType type 
    { 
        get { return _type; } 
    }

    protected override float extendCoef
    {
        get { return _extendCoef; }
    }

    protected override float actionDuration
    {
        get { return _fishTime; }
    }

    //public override float progress01
    //{
    //    get { return _currentFishTime / _fishTime; }
    //}

    [Header("Logic")]
    [SerializeField] CrewType _type;
    [SerializeField] float _maxSpeedToFish;
    [SerializeField] float _fishTime;
    [SerializeField] float _extendCoef = 1.5f;
    [SerializeField] float _minLineLength = 2f;
    [SerializeField] bool _restartAfterCatching;

    [Header("Linking")]
    [SerializeField] FishingLine _fishingLine;

    //QTCircleCollider _collider;
    //FishZone _fishZone;
    //bool _isFishing;
    //float _currentFishTime;

    Vector3 _floatPosition;

    bool canFish
    {
        get 
        {
            bool speedOK = (PlayerShip.instance.velocity.magnitude < _maxSpeedToFish);
            bool inputOK = !PlayerShip.instance.disableInputs;

            return speedOK && inputOK;
        }
    }

    protected override bool CanDoAction()
    {
        return canFish && Cargo.instance.CanStore(zone.fishType);
    }

    protected override void OnStartAction()
    {
        Vector3 shootPoint = WorldConversion.ToVector3(ShootLine());

        Vector3 target = zone.transform.position;

        _floatPosition = new Vector3(shootPoint.x, 0f, shootPoint.z);
        _fishingLine.Land(_floatPosition);
    }

    protected override bool CanStopAction()
    {
        return !_fishingLine.isLanding;
    }

    protected override void OnStopAction()
    {
        _fishingLine.StopFishing();
    }

    protected override void OnActionComplete()
    {
        Cargo.instance.AddFish(zone.fishType, 1);

        if (_restartAfterCatching)
            StopAction();
    }

    //void Awake()
    //{
    //    _collider = GetComponent<QTCircleCollider>();
    //}

    void OnEnable()
    {
        _fishingLine.StopFishing();
    }

    //void Update()
    //{
    //    if(_fishZone != null)
    //    {
    //        Fish(Time.deltaTime);
    //    }
    //    else if(_isFishing)
    //    {
    //        StopFishing();
    //    }
    //    else
    //    {
    //        List<QTCircleCollider> found = QuadTreeCircleManager.instance.Retrieve(_collider);

    //        QTCircleCollider best = null;
    //        float bestDistance = float.MaxValue;
    //        for(int i = 0; i < found.Count; i++)
    //        {
    //            float distance = Vector2.Distance(found[i].center, _collider.center);

    //            if(distance < bestDistance)
    //            {
    //                best = found[i];
    //                bestDistance = distance;
    //            }
    //        }

    //        if(best != null)
    //        {
    //            _fishZone = (FishZone)best;
    //        }
    //    }
    //}

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;

        _fishingLine.Actualize(Time.deltaTime);
    }

    //void Fish(float dt)
    //{
    //    if(_fishZone != null
    //        && _fishZone.radius + (_collider.radius * _extendCoef) 
    //            < Vector2.Distance(WorldConversion.ToVector2(transform.position), 
    //                               WorldConversion.ToVector2(_fishZone.transform.position)))
    //    {
    //        _fishZone = null;
    //    }
    //    else if (!_isFishing)
    //    {
    //        if(canFish && Cargo.instance.CanStore(_fishZone.fishType))
    //        {
    //            StartFishing();
    //        }
    //    }
    //    else
    //    {
    //        if(!canFish 
    //            || !Cargo.instance.CanStore(_fishZone.fishType))
    //        {
    //            StopFishing();
    //            return;
    //        }

    //        if(!_fishZone.isEnable)
    //        {
    //            StopFishing();
    //            _fishZone = null;
    //        }

    //        _currentFishTime += dt;

    //        if (_currentFishTime > _fishTime)
    //        {
    //            _currentFishTime -= _fishTime;
    //            Cargo.instance.AddFish(_fishZone.fishType, 1);

    //            if(_restartAfterCatching)
    //                StopFishing();
    //        }
    //    }
    //}

    //void StartFishing()
    //{
    //    if (_fishingLine.isStopping) return;

    //    _isFishing = true;
    //    _currentFishTime = 0f;

    //    Vector3 shootPoint = WorldConversion.ToVector3(ShootLine());

    //    Vector3 target = _fishZone.transform.position;

    //    _floatPosition = new Vector3(shootPoint.x, 0f, shootPoint.z);
    //    _fishingLine.Land(_floatPosition);
    //}

    Vector2 ShootLine()
    {
        Vector2 centerA = zone.center;
        float radiusA = zone.radius;

        Vector2 centerB = collider.center;
        float radiusB = collider.radius;

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

    //void StopFishing()
    //{
    //    if(_fishingLine.isLanding)
    //    {
    //        return;
    //    }

    //    _currentFishTime = 0f;
    //    _isFishing = false;
    //    _fishingLine.StopFishing();
    //}
}
