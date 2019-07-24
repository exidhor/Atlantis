using UnityEngine;
using System.Collections;

namespace Tools
{
    public class QTCircle : MonoBehaviour, IQTClearable
    {
#if UNITY_EDITOR
        public bool edit
        {
            get { return _edit; }
        }

        [SerializeField] bool _edit;
#endif

        [SerializeField] Vector2 _offset;
        [SerializeField] float _radius;
        [SerializeField] bool _persistent;

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
            //Gizmos.color = Color.green;

            //Vector3 center = transform.position 
            //                 + WorldConversion.ToVector3(_offset);
            //center.y = 0;

            //Gizmos.DrawWireSphere(center, _radius);

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
    }
}