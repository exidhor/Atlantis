using UnityEngine;
using System.Collections;

namespace Tools
{
    public class Vibrator : MonoBehaviour
    {
        [SerializeField] float _duration = 0.5f;
        [SerializeField] float _range = 10f;
        [SerializeField] int _repeat = 1;

        float _time = 0f;
        bool _started;
        int _doneTime;
        int _direction;

        Vector3 _start;

        Vector3 _from;
        Vector3 _offset;

        public void Vibrate()
        {
            if (_started)
            {
                transform.position = _start;
            }

            _time = 0f;
            _doneTime = 0;
            _started = true;
            _direction = 1;

            _start = transform.position;
            _from = _start;
            _offset = new Vector3(_range, 0f, 0f);
        }

        void Update()
        {
            if (_started)
            {
                _time += Time.deltaTime;

                if (_time >= _duration)
                {
                    _time = _duration;
                    Move();

                    _doneTime++;
                    _direction *= -1;

                    _from = transform.position;

                    if (_doneTime == _repeat)
                    {
                        _time -= _duration / 2;
                        _offset = new Vector3(_range, 0f, 0f);
                    }
                    else if (_doneTime > _repeat)
                    {
                        _started = false;

                        return;
                    }
                    else
                    {
                        _offset = new Vector3(2 * _range, 0f, 0f);
                        _time -= _duration;
                    }
                }

                Move();
            }
        }

        void Move()
        {
            transform.position = Vector3.Lerp(_from,
                                              _from + _direction * _offset,
                                              _time / _duration);
        }
    }

}