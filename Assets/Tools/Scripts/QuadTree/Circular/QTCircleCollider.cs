using UnityEngine;
using System.Collections;

namespace Tools
{
    public class QTCircleCollider : MonoBehaviour, IQTClearable
    {
#if UNITY_EDITOR
        public bool edit
        {
            get { return _edit; }
        }

        [SerializeField] bool _edit;
#endif

        [SerializeField] int _layer;
        [SerializeField] Vector2 _offset;
        [SerializeField] float _radius;
        [SerializeField] bool _persistent;

        bool _isRegistered;

        public int layer
        {
            get { return _layer; }
        }

        public bool isEnable
        {
            get { return enabled; }
        }

        public bool persistent
        {
            get { return _persistent; }
        }

        public Vector2 center
        {
            get 
            {
                return WorldConversion.ToVector2(transform.position)
                       + _offset;
                }
        }

        public float radius
        {
            get { return _radius; }
        }

        public Rect GetGlobalBounds()
        {
            return new Rect(center - Vector2.one * radius,
                            Vector2.one * 2 * radius);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Rect rect = GetGlobalBounds();

            Gizmos.DrawWireCube(WorldConversion.ToVector3(rect.center),
                                WorldConversion.ToVector3(rect.size));
        }

        public void SetRadius(float radius)
        {
            _radius = radius;
        }

        public void SetOffset(Vector2 offset)
        {
            _offset = offset;
        }

        public void Update()
        {
            QuadTreeCircleManager.instance.Register(this);
            Debug.Log("Add collider at " + Time.time);
        }

        void OnEnable()
        {
            if(!_isRegistered)
            {
                _isRegistered = true;
                QuadTreeCircleManager.instance.Register(this);
            }
        }
    }
}