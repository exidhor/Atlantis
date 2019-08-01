using UnityEngine;
using System.Collections;

namespace Tools
{
    public class ScaleAnim : MonoBehaviour
    {
        [SerializeField] float _targetGrowScale;
        [SerializeField] float _targetReduceScale;
        [SerializeField] float _duration;
        [SerializeField] AnimationCurve _curve;

        float _time;
        bool _started;
        bool _grow;

        float _delay;

        public void Grow(float delay = 0f)
        {
            _started = true;
            _grow = true;
            _time = 0f;

            _delay = delay;
        }

        public void Reduce(float delay = 0f)
        {
            _started = true;
            _grow = false;
            _time = 0f;

            _delay = delay;
        }

        void Update()
        {
            if (_started)
            {
                _time += Time.deltaTime;

                if(_time < _delay)
                {
                    return;
                }

                if (_time >= _duration + _delay)
                {
                    _started = false;
                    _time = _delay + _duration;
                }

                float ct = _curve.Evaluate((_time - _delay) / _duration);

                float s = Mathf.LerpUnclamped(1f, _grow ? _targetGrowScale : _targetReduceScale, ct);
                transform.localScale = Vector3.one * s;
            }
        }
    }
}