using UnityEngine;
using System.Collections;

namespace NexusCity
{
    public class Agent : MonoBehaviour
    {
        [SerializeField] Targeter _targeter;
        [SerializeField] KinematicBody _body;

        AgentData _data;

        public void Init(AgentData data)
        {
            _data = data;

            _targeter.Init(data);
            _body.Init(data);
        }

        public void Actualize(float dt)
        {
            _targeter.Actualize(dt);

            Vector2 t = _targeter.currentTarget.position;
            _body.Actualize(t, dt);
        }
    }
}