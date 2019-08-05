using UnityEngine;
using System.Collections;

public class Whale : AgentMonster
{
    public override MonsterType type
    {
        get { return MonsterType.Whale; }
    }

    protected override void OnPathEnd()
    {
        // todo
    }
}
