using UnityEngine;
using System.Collections;
using Tools;

public class FishZone : QTCircleCollider
{
    public FishType fishType
    {
        get { return _fishType; }
    }

    [Header("Fish Zone")]
    [SerializeField] FishType _fishType;
    [SerializeField] float _depthEnable;
    [SerializeField] float _depthDisable;
    [SerializeField] Vector2 _enableDurationRange;
    [SerializeField] Vector2 _disableDurationRange;
    [SerializeField] float _movingDuration;

    float _time;
    float _currentDuration;
    bool _isMovingUp;

    void Awake()
    {
        bool startEnable = Random.value > 0.5f;

        if(startEnable)
        {
            SetEnableTrue();
            _time = Random.Range(0f, _currentDuration);
        }
        else
        {
            SetEnableFalse();
            _time = Random.Range(0f, _currentDuration);
        }
    }

    protected override void Update()
    {
        base.Update();

        _time += Time.deltaTime;

        if(_enable)
        {
            if(_time >= _currentDuration)
            {
                SetEnableFalse();
            }
            else
            {
                float t = _time - (_currentDuration - _movingDuration);
                if (t < 0f) t = 0f;

                float depth = Mathf.Lerp(_depthEnable, _depthDisable, t);
                SetDepth(depth);
            }
        }
        else
        {
            if (_time >= _currentDuration)
            {
                SetEnableTrue();
            }
            else
            {
                float t = _time - (_currentDuration - _movingDuration);
                if (t < 0f) t = 0f;

                float depth = Mathf.Lerp(_depthDisable, _depthEnable, t);
                SetDepth(depth);
            }
        }
    }

    void SetEnableTrue()
    {
        _enable = true;
        _currentDuration = Random.Range(_enableDurationRange.x, _enableDurationRange.y);
        _time = 0f;

        SetDepth(_depthEnable);
    }

    void SetEnableFalse()
    {
        _enable = false;
        _currentDuration = Random.Range(_disableDurationRange.x, _disableDurationRange.y);
        _time = 0f;

        SetDepth(_depthDisable);
    }

    void SetDepth(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }
}
