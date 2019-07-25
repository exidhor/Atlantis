using UnityEngine;
using System.Collections.Generic;
using System;

namespace Tools
{
    public class QuadTreeCircleManager : MonoSingleton<QuadTreeCircleManager>
#if UNITY_EDITOR
                                         ,ISerializationCallbackReceiver
#endif
    {
        #region QuadTrees

        [Serializable]
        public class QuadTrees
        {
            public QuadTreeCircle dynamicQT;
            public QuadTreeCircle staticQT;

            public QuadTrees(Rect bounds)
            {
                dynamicQT = new QuadTreeCircle(bounds);
                staticQT = new QuadTreeCircle(bounds);
            }

            public void Insert(QTCircleCollider collider)
            {
                QuadTree<QTCircleCollider> quadTree = collider.@static ? staticQT : dynamicQT;

                quadTree.Insert(collider, collider.GetGlobalBounds());
            }

            public List<QTCircleCollider> Retrieve(Rect rect)
            {
                List<QTCircleCollider> found = dynamicQT.Retrieve(rect);
                found.AddRange(staticQT.Retrieve(rect));

                return found;
            }

            public void RetrieveNonAlloc(List<QTCircleCollider> toFill, QTCircleCollider collider)
            {
                Rect bounds = collider.GetGlobalBounds();

                dynamicQT.RetrieveNonAlloc(toFill, bounds);
                staticQT.RetrieveNonAlloc(toFill, bounds);
            }

            public void Clear(bool clearStatic = false)
            {
                dynamicQT.Clear(true);

                if(clearStatic)
                    staticQT.Clear(true);
            }

            public void OnDrawGizmos(float heightStatic, float heightDynamic)
            {
                if (dynamicQT != null)
                    dynamicQT.OnDrawGizmos(Color.red, Color.magenta, heightDynamic);

                if (staticQT != null)
                    staticQT.OnDrawGizmos(Color.green, Color.blue, heightStatic);
            }

            public void Serialize()
            {
                if (dynamicQT != null)
                    dynamicQT.Serialize();

                if (staticQT != null)
                    staticQT.Serialize();
            }
        }

        #endregion

        [Header("Debug")]
        [SerializeField] bool serialize;
        [SerializeField] bool _drawGizmos;
        [SerializeField] int _debugThisLayerIndex;
        [SerializeField] float _heightGizmosStatic = 50f;
        [SerializeField] float _heightGizmosDynamic = 0f;

        [Header("Infos")]
        [SerializeField] int _layerCount;
        [SerializeField] Rect _worldBounds;

        [Header("Serialization")]
        [SerializeField, UnityReadOnly]
        List<QuadTrees> _quadTrees = new List<QuadTrees> ();

        List<QTCircleCollider> _buffer = new List<QTCircleCollider>(100); 

        void Awake()
        {
            for(int i = 0; i < _layerCount; i++)
            {
                _quadTrees.Add(new QuadTrees(_worldBounds));
            }
        }

        public void Register(QTCircleCollider collider)
        {
            QuadTrees qt = _quadTrees[collider.layer];
            qt.Insert(collider);
        }

        public void Update()
        {
            Clear();
        }

        public void Clear()
        {
            for (int i = 0; i < _quadTrees.Count; i++)
            {
                _quadTrees[i].Clear();
            }
        }

        public List<QTCircleCollider> Retrieve(QTCircleCollider collider)
        {
            _buffer.Clear();

            QuadTrees qt = _quadTrees[collider.layer];
            qt.RetrieveNonAlloc(_buffer, collider);

            FilterListWithCircleCircleCollision(_buffer, collider);

            return _buffer;
        }

        // ------------------------------------------------
        // This algo remove objects in O(n)
        // ------------------------------------------------
        void FilterListWithCircleCircleCollision(List<QTCircleCollider> list, 
                                                 QTCircleCollider collider)
        {
            int removeIndex = -1;
            int removeCount = 0;

            for (int i = 0; i < list.Count; i++)
            {
                QTCircleCollider c = list[i];

                if (!Overlap(collider, c))
                {
                    removeCount++;

                    if (removeIndex == -1)
                    {
                        removeIndex = i;
                    }
                }
                else
                {
                    if (removeIndex != -1)
                    {
                        // swap with the first remove item
                        list[removeIndex] = c;
                        removeIndex++;
                    }
                }
            }

            if (removeIndex > -1)
            {
                list.RemoveRange(removeIndex, removeCount);
            }
        }

        bool Overlap(QTCircleCollider a, QTCircleCollider b)
        {
            return Vector2.Distance(a.center, b.center) < a.radius + b.radius;
        }

        void OnDrawGizmos()
        {
            if (!_drawGizmos) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(WorldConversion.ToVector3(_worldBounds.center), 
                                WorldConversion.ToVector3(_worldBounds.size));
            Gizmos.color = Color.white;

            if(_debugThisLayerIndex >= 0 && _debugThisLayerIndex < _quadTrees.Count)
            {
                _quadTrees[_debugThisLayerIndex].OnDrawGizmos(_heightGizmosStatic,
                                                              _heightGizmosDynamic);
            }
        }

#if UNITY_EDITOR
        public void OnBeforeSerialize()
        {
            if (!serialize) return;

            for(int i = 0; i < _quadTrees.Count; i++)
            {
                _quadTrees[i].Serialize();
            }
        }

        public void OnAfterDeserialize()
        {
            // nothing
        }
#endif
    }
}