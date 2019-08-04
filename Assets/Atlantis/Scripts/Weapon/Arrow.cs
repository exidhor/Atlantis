using UnityEngine;
using System.Collections;

public class Arrow : Bullet
{
    public override BulletType type
    {
        get { return BulletType.Arrow; }
    }

    [SerializeField] string _trajectoryCurveName;

    EvaluationCurve _trajectoryCurve;

    protected override void Move(float dt)
    {
        // todo
    }
}
