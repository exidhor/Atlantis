using UnityEngine;
using System.Collections;

public class Fisherman : Crew
{
    [Header("Logic")]
    [SerializeField] float _maxSpeedToFish;
    [SerializeField] float _fishTime;

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
            return (PlayerShip.instance.velocity.magnitude < _maxSpeedToFish)
                    && !PlayerShip.instance.disableInputs; 
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

    void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.layer == LayerType.instance.layerFish)
        {
            _fishZone = other.gameObject.GetComponent<FishZone>();
        }
    }

    void OnTriggerExit2D(Collider other)
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
                StopFishing();
                return;
            }

            float lineDistance = Vector2.Distance(_floatPosition, transform.position);

            if (_collider.radius > lineDistance)
            {
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
        _floatPosition = _fishZone.transform.position;
        _fishingLine.Land(_floatPosition);
    }

    void StopFishing()
    {
        _isFishing = false;
        _fishingLine.StopFishing();
    }
}
