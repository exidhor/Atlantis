using UnityEngine;
using System.Collections.Generic;
using System;

namespace Tools
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [Serializable]
        class AnimationData
        {
            public float spriteTime
            {
                get { return _spriteTime; }
            }

            public List<Sprite> sprites
            {
                get { return _sprites; }
            }

            [SerializeField] float _spriteTime = 0.1f;
            [SerializeField] List<Sprite> _sprites = new List<Sprite>();
        }

        [Serializable]
        class AnimationEntry
        {
            public string name
            {
                get { return _name; }
            }

            public bool loop
            {
                get { return _loop; }
            }

            public AnimationData data
            {
                get { return _data; }
            }

            [SerializeField] string _name;
            [SerializeField] bool _loop;
            [SerializeField] AnimationData _data;
        }

        SpriteRenderer spriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }

                return _spriteRenderer;
            }
        }

        [SerializeField] AnimationData _idle;
        [SerializeField] List<AnimationEntry> _anims = new List<AnimationEntry>();

        Dictionary<string, AnimationEntry> _map;
        SpriteRenderer _spriteRenderer;

        int _index = 0;
        float _currentTime;
        int _direction = 1;

        bool _loop;
        Action _onEnd;
        string _currentName;
        AnimationData _current = null;

        void Awake()
        {
            if (_current == null)
            {
                StartIdle();

                float totalTime = 0f;
                for(int i = 0; i < _anims.Count; i++)
                {
                    AnimationData d = _anims[i].data;
                    totalTime += d.spriteTime * d.sprites.Count;
                }

                _currentTime = UnityEngine.Random.Range(0f, totalTime);
            }
        }

        public void StartIdle()
        {
            _index = 0;
            _currentTime = 0f;
            _direction = 1;

            _loop = true;
            _onEnd = null;
            _current = _idle;

            _currentName = "idle";

            Refresh();
        }

        void CheckMap()
        {
            if (_map == null)
            {
                _map = new Dictionary<string, AnimationEntry>();

                for (int i = 0; i < _anims.Count; i++)
                {
                    _map.Add(_anims[i].name, _anims[i]);
                }
            }
        }

        public string GetCurrentName()
        {
            return _currentName;
        }

        public int GetCurrentIndex()
        {
            return _index;
        }

        public void StartAnim(string name, Action onEnd = null, bool reverse = false)
        {
            _currentName = name;
            CheckMap();

            if (_map.ContainsKey(name))
            {
                InitWith(_map[name], onEnd, reverse ? -1 : 1);
            }
        }

        public void ReverseCurrentAnim(Action onEnd = null)
        {
            _onEnd = onEnd;

            _direction = -_direction;
        }

        public void StartAnim(int index, Action onEnd = null)
        {
            _currentName = _anims[index].name; 

            CheckMap();

            InitWith(_anims[index], onEnd);
        }

        void InitWith(AnimationEntry entry, Action onEnd, int direction = 1)
        {
            _direction = direction;
            _currentTime = 0f;

            _loop = entry.loop;
            _onEnd = onEnd;
            _current = entry.data;

            _index = GetStartIndex();

            Refresh();
        }

        int GetStartIndex()
        {
            return _direction > 0 ? 0 : _current.sprites.Count - 1;
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            Refresh();
        }

        void Refresh()
        {
            while (_currentTime > _current.spriteTime)
            {
                _index += _direction;

                if ((_direction > 0 && _index >= _current.sprites.Count)
                 || (_direction < 0 && _index < 0))
                {
                    if (_loop)
                    {
                        _index = GetStartIndex();
                    }
                    else
                    {
                        Action todo = _onEnd;

                        StartIdle();

                        if (todo != null)
                            todo();

                        return;
                    }
                }

                _currentTime -= _current.spriteTime;
            }

            if (_current.sprites.Count > 0)
            {
                spriteRenderer.sprite = _current.sprites[_index];
            }
        }
    }
}
