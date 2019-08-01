using UnityEngine;
using System.Collections.Generic;
using Tools;
using MemoryManagement;

public class CrewLibrary : MonoSingleton<CrewLibrary>
{
    public Sprite genericCrewIcon
    {
        get { return _genericCrewIcon; }
    }

    [Header("Generics")]
    [SerializeField] Sprite _genericCrewIcon;

    [Header("Pool Infos")]
    [SerializeField] uint _poolCapacity;
    [SerializeField] uint _expand;

    [Header("Crew Infos")]
    [SerializeField] 
    List<CrewInfo> _infos = new List<CrewInfo>();

    List<UnityPool> _poolByType = new List<UnityPool>();

    float _totalRate;

    void Awake()
    {
        ConstructPools();
    }

    void ConstructPools()
    {
        _infos.Sort((a, b) => a.model.type.CompareTo(b.model.type));

        _totalRate = 0f;

        for (int i = 0; i < _infos.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.parent = transform;

            UnityPool pool = go.AddComponent<UnityPool>();
            pool.Construct(_infos[i].model, _expand);
            pool.SetSize(_poolCapacity);

            _poolByType.Add(pool);

            _totalRate += _infos[i].rate;
        }
    }

    public Crew GetFreeCrew(CrewType type)
    {
        Crew crew = (Crew) _poolByType[(int)type].GetFreeResource();

        crew.transform.localPosition = Vector3.zero;

        return crew;
    }

    public CrewInfo GetRandomInfo()
    {
        float rand = Random.Range(0f, _totalRate);
        float sum = 0f;

        for(int i = 0; i < _infos.Count; i++)
        {
            sum += _infos[i].rate;

            if (rand <= sum)
            {
                return _infos[i];
            }
        }

        return _infos[_infos.Count - 1];
    }

    public CrewInfo GetInfo(CrewType type)
    {
        return _infos[(int)type];
    }
}
