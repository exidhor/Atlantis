using UnityEngine;
using System.Collections;

namespace Tools
{
    public class QuadTreeCircleManager : MonoSingleton<QuadTreeCircleManager>
    {
        [SerializeField] Rect _worldBounds;

        QuadTree<QTCircle> _quadTree;

        void Awake()
        {
            _quadTree = new QuadTree<QTCircle>(_worldBounds);
        }

        void Register(QTCircle collider)
        {
            _quadTree.Insert(collider, collider.GetGlobalBounds());
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_worldBounds.center, _worldBounds.size);
        }
    }
}