using UnityEngine;
using System.Collections.Generic;
using System;
using Tools;

public class EvaluationCurveManager : MonoSingleton<EvaluationCurveManager>
{
    [Serializable]
    class EvaluationCurveNamed
    {
        public string name;
        public EvaluationCurve curve;
    }

    [SerializeField] List<EvaluationCurveNamed> _curves;

    Dictionary<string, EvaluationCurve> _map = new Dictionary<string, EvaluationCurve>();

    void Awake()
    {
        for(int i = 0; i < _curves.Count; i++)
        {
            _map.Add(_curves[i].name, _curves[i].curve);
        }
    }

    public EvaluationCurve GetCurve(string name)
    {
        return _map[name];
    }
}
