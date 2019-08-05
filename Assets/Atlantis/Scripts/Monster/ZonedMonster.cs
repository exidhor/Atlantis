using UnityEngine;
using System.Collections;

// todo : implement this : 
// https://unity3d.com/fr/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
public abstract class ZonedMonster : AgentMonster
{
    [Header("Wander Specs")]
    [SerializeField] float _wanderSpeed;

    MonsterZone _zone;

    public void SetZone(MonsterZone zone)
    {
        _zone = zone;
    }

    protected override void OnPathEnd()
    {
        
    }

    protected override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);

        
    }
}
