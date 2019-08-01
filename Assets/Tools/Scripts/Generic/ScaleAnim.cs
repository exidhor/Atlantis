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

        public void Grow()
        {
            _started = true;
            _grow = true;
            _time = 0f;
        }

        public void Reduce()
        {
            _started = true;
            _grow = false;
            _time = 0f;
        }

        void Update()
        {
            if (_started)
            {
                _time += Time.deltaTime;

                if (_time >= _duration)
                {
                    _started = false;
                    _time = _duration;
                }

                float ct = _curve.Evaluate(_time / _duration);

                float s = Mathf.LerpUnclamped(1f, _grow ? _targetGrowScale : _targetReduceScale, ct);
                transform.localScale = Vector3.one * s;
            }
        }
    }
}