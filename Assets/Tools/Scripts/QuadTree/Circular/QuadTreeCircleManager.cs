using UnityEngine;
using System.Collections.Generic;
using System;

namespace Tools
{
    [Serializable]
    public class QuadTreeCircle : QuadTree<QTCircleCollider>
    {
        #region Serialization

#if UNITY_EDITOR
        [Serializable]
        public class SerializableQuadTreeNode
        {
            public string name = "Node NULL";
            public bool isNotNull = false;
            public int depthLevel;
            public SerializableQuadTreeNode parent;
            public Rect bounds;

            public SerializableQuadTreeNode[] children = new SerializableQuadTreeNode[0];
            public List<QTCircleCollider> objects = new List<QTCircleCollider>();

            public SerializableQuadTreeNode(QuadTreeNode<QTCircleCollider> node, 
                                            SerializableQuadTreeNode parent)
            {
                children = new SerializableQuadTreeNode[4];

                isNotNull = true;
                this.parent = parent;

                depthLevel = node.level;
                bounds = node.bounds;
                
                for(int i = 0; i < node.objects.Count; i++)
                {
                    objects.Add(node.objects[i].obj);
                }

                int childCount = 0;

                for(int i = 0; i < node.nodes.Length; i++)
                {
                    if(node.nodes[i] != null)
                    {
                        children[i] = new SerializableQuadTreeNode(node.nodes[i], this);
                        childCount++;
                    }
                    else
                    {
                        children[i] = null;
                    }
                }

                name = "Node (child=" + childCount + ") (objects=" + node.objects.Count + ")";
            }
        }
#endif

        [SerializeField] SerializableQuadTreeNode _rootNode;

        public void Serialize()
        {
            if(_root != null)
            {
                _rootNode = new SerializableQuadTreeNode(_root, null);
            }
        }

        #endregion

        public QuadTreeCircle(Rect bounds) : base(bounds)
        { }

        public void OnDrawGizmos()
        {
            DrawGizmosNode(_root);
        }

        void DrawGizmosNode(QuadTreeNode<QTCircleCollider> node)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(WorldConversion.ToVector3(node.bounds.center),
                                WorldConversion.ToVector3(node.bounds.size));

            Gizmos.color = Color.red;
            for(int i = 0; i < node.objects.Count; i++)
            {
                Rect rect = node.objects[i].rect;

                Gizmos.DrawWireCube(WorldConversion.ToVector3(rect.center),
                                    WorldConversion.ToVector3(rect.size));
            }

            for (int i = 0; i < node.nodes.Length; i++)
            {
                if(node.nodes[i] != null)
                {
                    DrawGizmosNode(node.nodes[i]);
                }
            }

            Gizmos.color = Color.white;
        }
    }

    public class QuadTreeCircleManager : MonoSingleton<QuadTreeCircleManager>
#if UNITY_EDITOR
                                         ,ISerializationCallbackReceiver
#endif
    {
        [SerializeField] bool _drawGizmos;
        [SerializeField] int _debugThisLayerIndex;

        [SerializeField] int _layerCount;
        [SerializeField] Rect _worldBounds;

        [SerializeField, UnityReadOnly]
        List<QuadTreeCircle> _quadTrees = new List<QuadTreeCircle> ();

        List<QTCircleCollider> _buffer = new List<QTCircleCollider>(100); 

        void Awake()
        {
            for(int i = 0; i < _layerCount; i++)
            {
                _quadTrees.Add(new QuadTreeCircle(_worldBounds));
            }
        }

        public void Register(QTCircleCollider collider)
        {
            QuadTreeCircle qt = _quadTrees[collider.layer];
            qt.Insert(collider, collider.GetGlobalBounds());
        }

        public void Update()
        {
            Debug.Log("Clear qt at " + Time.time);
            Clear(false);
        }

        public void Clear(bool force)
        {
            for (int i = 0; i < _quadTrees.Count; i++)
            {
                _quadTrees[i].Clear(force);
            }
        }

        public List<QTCircleCollider> Retrieve(QTCircleCollider collider)
        {
            _buffer.Clear();

            QuadTreeCircle qt = _quadTrees[collider.layer];
            qt.RetrieveNonAlloc(_buffer, collider.GetGlobalBounds());

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
                _quadTrees[_debugThisLayerIndex].OnDrawGizmos();
            }
        }

#if UNITY_EDITOR
        public void OnBeforeSerialize()
        {
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