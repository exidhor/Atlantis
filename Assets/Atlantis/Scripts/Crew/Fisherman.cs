using UnityEngine;
using System.Collections;

public class Fisherman : Crew
{
    [Header("Logic")]
    [SerializeField] float _maxSpeedToFish;
    [SerializeField] float _fishTime;

    [Header("Linking")]
    [SerializeField] GameObject _floatModel;

    FishZone _fishZone;
    bool _isFishing;
    float _currentFishTime;

    bool canFish
    {
        get { return (PlayerShip.instance.speed < _maxSpeedToFish); }
    }

    void OnEnable()
    {
        _floatModel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerType.instance.layerFish)
        {
            _fishZone = other.gameObject.GetComponent<FishZone>();
        }
    }

    void OnTriggerExit(Collider other)
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
            }
            else
            {
                _currentFishTime += dt;

                if (_currentFishTime > _fishTime)
                {
                    _currentFishTime -= _fishTime;
                    Cargo.instance.AddFish(1);
                }
            }
        }
    }

    void StartFishing()
    {
        _isFishing = true;
        _currentFishTime = 0f;
        _floatModel.gameObject.SetActive(true);
    }

    void StopFishing()
    {
        _isFishing = false;
        _floatModel.gameObject.SetActive(false);
    }
}
