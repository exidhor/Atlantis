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

                for (int i = 0; i < node.objects.Count; i++)
                {
                    objects.Add(node.objects[i].obj);
                }

                int childCount = 0;

                for (int i = 0; i < node.nodes.Length; i++)
                {
                    if (node.nodes[i] != null)
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
            if (_root != null)
            {
                _rootNode = new SerializableQuadTreeNode(_root, null);
            }
        }

        #endregion

        public QuadTreeCircle(Rect bounds) : base(bounds)
        { }

        public void OnDrawGizmos(Color nodeColor, 
                                 Color objectColor,
                                 float height)
        {
            DrawGizmosNode(_root, nodeColor, objectColor, height);
        }

        void DrawGizmosNode(QuadTreeNode<QTCircleCollider> node, 
                            Color nodeColor, 
                            Color objectColor,
                            float height)
        {
            Vector3 heightVector = new Vector3(0f, height, 0f);

            Gizmos.color = nodeColor;
            Gizmos.DrawWireCube(WorldConversion.ToVector3(node.bounds.center) + heightVector,
                                WorldConversion.ToVector3(node.bounds.size));

            Gizmos.color = objectColor;
            for (int i = 0; i < node.objects.Count; i++)
            {
                if(node.objects[i].obj.isEnable)
                {
                    Rect rect = node.objects[i].rect;

                    Gizmos.DrawWireCube(WorldConversion.ToVector3(rect.center) + heightVector,
                                        WorldConversion.ToVector3(rect.size));
                }
            }

            for (int i = 0; i < node.nodes.Length; i++)
            {
                if (node.nodes[i] != null)
                {
                    DrawGizmosNode(node.nodes[i], 
                                   nodeColor, 
                                   objectColor,
                                   height);
                }
            }

            Gizmos.color = Color.white;
        }
    }
}
