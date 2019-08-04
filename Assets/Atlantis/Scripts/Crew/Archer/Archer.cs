using UnityEngine;
using System.Collections.Generic;
using Tools;

[RequireComponent(typeof(QTCircleCollider))]
public class Archer : CrewWithRange<Monster>
{
    public override CrewType type
    {
        get { return CrewType.Archer; }
    }

    protected override float extendCoef
    {
        get { return 1f; }
    }

    protected override float actionDuration
    {
        get { return _targetTime; }
    }

    [Header("Logic")]
    [SerializeField] float _targetTime;
    [SerializeField] Weapon _weapon;

    protected override bool CanDoAction()
    {
        return true;
    }

    protected override void OnStartAction()
    {
        Vector3 target = zone.transform.position;

        // todo
    }

    protected override bool CanStopAction()
    {
        return true;
    }

    protected override void OnStopAction()
    {
        //_fishingLine.StopFishing();
    }

    protected override void OnActionComplete()
    {
        _weapon.Fire(zone);

        //Cargo.instance.AddFish(zone.fishType, 1);

        //if (_restartAfterCatching)
            //StopAction();
    }
}
