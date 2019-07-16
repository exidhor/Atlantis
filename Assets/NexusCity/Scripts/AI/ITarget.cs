using UnityEngine;
using System.Collections;
using System;

namespace NexusCity
{
    public interface ITarget
    {
        bool isValid { get; }
        Vector2 position { get; }
    }

    public class TransformTarget : ITarget
    {
        public bool isValid
        {
            get { return _isValid(); }
        }

        public Vector2 position
        {
            get { return _transform.position; }
        }

        Transform _transform;
        Func<bool> _isValid;

        public TransformTarget(Transform transform, Func<bool> isValid = null)
        {
            _transform = transform;

            if(isValid == null)
            {
                _isValid = () => { return true; };
            }
            else
            {
                _isValid = isValid;
            }
        }
    }

    public class PointTarget : ITarget
    {
        public bool isValid
        {
            get { return true; }
        }

        public Vector2 position
        {
            get { return _point; }
        }

        Vector2 _point;

        public PointTarget(Vector2 point)
        {
            _point = point;
        }
    }
}