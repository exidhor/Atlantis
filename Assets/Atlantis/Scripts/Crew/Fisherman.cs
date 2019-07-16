using UnityEngine;
using System.Collections;

public class Fisherman : Crew
{
    [SerializeField] GameObject _floatModel;
    [SerializeField] float _fishTime;

    FishZone _fishZone;
    float _currentFishTime;

    void OnEnable()
    {
        _floatModel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerType.instance.layerFish)
        {
            _fishZone = other.gameObject.GetComponent<FishZone>();
            _floatModel.SetActive(true);
            _currentFishTime = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_fishZone != null
            && _fishZone.gameObject == other.gameObject)
        {
            _fishZone = null;
            _floatModel.SetActive(false);
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
        _currentFishTime += dt;

        if(_currentFishTime > _fishTime)
        {
            _currentFishTime -= _fishTime;
            Cargo.instance.AddFish(1);
        }
    }
}
