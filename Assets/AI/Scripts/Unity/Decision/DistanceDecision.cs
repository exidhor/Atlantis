using UnityEngine;
using System.Collections;
using Tools;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Distance")]
    public class DistanceDecision : Decision
    {
        [SerializeField] float _maxDistance = 10f;

        public override bool Decide(Steering steering)
        {
            Vector2 playerPos2D = WorldConversion.ToVector2(PlayerShip.instance.transform.position);
            Vector2 characterPos2D = WorldConversion.ToVector2(steering.controllerTransform.position);

            float distance = Vector2.Distance(playerPos2D, characterPos2D);

            return _maxDistance < distance;
        }
    }
}