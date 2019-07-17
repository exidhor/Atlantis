using UnityEngine;
using System.Collections.Generic;
using Tools;

public class PlayerShipBody : MonoBehaviour
{
    public Vector2 velocity
    {
        get { return _velocity; }
    }

    public float angular
    {
        get { return _angular; }
    }

    [Header("On Repulse")]
    [SerializeField] string _velocityCurveRepulse;
    [SerializeField] string _angularCurveRepulse;
    [SerializeField] float _disableInputsTime;
    [SerializeField] float _maxMagnitude;

    [Header("Debug")]
    [SerializeField, UnityReadOnly] Vector2 _velocity;
    [SerializeField, UnityReadOnly] float _angular;
    [SerializeField, UnityReadOnly] List<Force> _forces = new List<Force>(50);

    class RepulseToken
    {
        public Vector3 origin;
        public Vector3 direction;
        public float time = 3f;

        public RepulseToken(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public bool Actualize(float dt)
        {
            time -= dt;

            return time <= 0f;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, origin + direction);
        }
    }

    List<RepulseToken> _tokens = new List<RepulseToken>();

    public bool ApplyRepulseForce(Vector2 velocity, 
                                  float angular, 
                                  GameObject origin)
    {
        Force repulse = PoolTable.instance.GetForce(velocity, 
                                                    angular, 
                                                    _velocityCurveRepulse, 
                                                    _angularCurveRepulse,
                                                    origin);

        bool added = AddForce(repulse);

        if (added)
        {
            PlayerShip.instance.DisableInputs(_disableInputsTime);
        }

        return added;
    }

    bool AddForce(Force force)
    {
        if(force.origin != null)
        {
            for (int i = 0; i < _forces.Count; i++)
            {
                //if (_forces[i].origin == force.origin)
                    //return false;
            }
        }

        _forces.Add(force);
        _tokens.Add(new RepulseToken(force.origin.transform.position, force.velocity));

        return true;
    }

    void Update()
    {
        for(int i = 0; i < _tokens.Count; i++)
        {
            if (_tokens[i].Actualize(Time.deltaTime))
            {
                _tokens.RemoveAt(i);
                i--;
            }
        }
    }

    void OnDrawGizmos()
    {
        for(int i = 0; i < _tokens.Count; i++)
        {
            _tokens[i].OnDrawGizmos();
        }
    }

    public void Refresh(float dt)
    {
        for(int i = 0; i < _forces.Count; i++)
        {
            _forces[i].Refresh(dt);
        }

        _velocity = Vector2.zero;
        _angular = 0f;

        for(int i = 0; i < _forces.Count; i++)
        {
            _velocity += _forces[i].currentVelocity;
            _angular += _forces[i].currentAngular;
        }

        if(_velocity.magnitude > _maxMagnitude)
        {
            _velocity.Normalize();
            _velocity *= _maxMagnitude;
        }

        for (int i = 0; i < _forces.Count; i++)
        {
            if(_forces[i].isFinish)
            {
                _forces.RemoveAt(i);
                i--;
            }
        }
    }
}
