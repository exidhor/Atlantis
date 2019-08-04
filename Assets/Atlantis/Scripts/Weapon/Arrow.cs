using UnityEngine;
using System.Collections;
using Tools;

public class Arrow : Bullet
{
    public override BulletType type
    {
        get { return BulletType.Arrow; }
    }

    [Header("Arrow Specs")]
    [SerializeField] float _heightTrajectory01;

    // graph : https://www.desmos.com/calculator/ogszbuwnx4

    float _height; // = a 
    float _offset; // = b
    float _coef;   // = c

    protected override void OnInit()
    {
        // todo
    }

    protected override void Move(float dt)
    {
        Vector2 start2D = WorldConversion.ToVector2(start);
        Vector2 end2D = WorldConversion.ToVector2(end);

        float distance = Vector2.Distance(start2D, end2D);

        float height = _heightTrajectory01 * distance;

        Vector3 pos = MathHelper.Parabola(start, end, height, currentTime / duration);

        transform.position = pos;
    }
}
