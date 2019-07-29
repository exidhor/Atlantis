using UnityEngine;
using System.Collections.Generic;
using Tools;
using MemoryManagement;

public class CrewLibrary : MonoSingleton<CrewLibrary>
{
    [SerializeField] uint _poolCapacity;
    [SerializeField] uint _expand;

    [SerializeField] 
    List<CrewInfo> _infos = new List<CrewInfo>();

    List<UnityPool> _poolByType = new List<UnityPool>();

    void Awake()
    {
        ConstructPools();
    }

    void ConstructPools()
    {
        _infos.Sort((a, b) => a.model.type.CompareTo(b.model.type));

        for(int i = 0; i < _infos.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.parent = transform;

            UnityPool pool = go.AddComponent<UnityPool>();
            pool.Construct(_infos[i].model, _expand);
            pool.SetSize(_poolCapacity);

            _poolByType.Add(pool);
        }
    }

    public Crew GetFreeCrew(CrewType type)
    {
        Crew crew = (Crew) _poolByType[(int)type].GetFreeResource();

        crew.transform.localPosition = Vector3.zero;

        return crew;
    }

    public CrewInfo GetInfo(CrewType type)
    {
        return _infos[(int)type];
    }
}
