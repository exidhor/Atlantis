using UnityEngine;
using System.Collections;
using Tools;
using MemoryManagement;

public class PoolTable : MonoSingleton<PoolTable>
{
    [Header("Force")]
    [SerializeField] int _forceSize;

    [Header("Global")]
    [SerializeField] int _expand;

    PoolStack<Force> _forces;

    void Awake()
    {
        _forces = new PoolStack<Force>(ForceConstructor, _forceSize, _expand);
    }

    #region Constructors
    Force ForceConstructor()
    {
        return new Force();
    }
    #endregion

    #region Getters
    public Force GetForce(Vector2 velocity, 
                          float angular, 
                          string velocityCurveName, 
                          string angularCurveName,
                          GameObject origin = null)
    {
        Force force = _forces.Get();

        force.Init(velocity,
                   angular,
                   EvaluationCurveManager.instance.GetCurve(velocityCurveName),
                   EvaluationCurveManager.instance.GetCurve(angularCurveName),
                   origin);

        return force;
    }
    #endregion
}
