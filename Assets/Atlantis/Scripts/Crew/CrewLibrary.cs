using UnityEngine;
using System.Collections.Generic;
using Tools;
using MemoryManagement;

public class CrewLibrary : MonoSingleton<CrewLibrary>
{
    [SerializeField] uint _poolCapacity;
    [SerializeField] uint _expand;

    [SerializeField] 
    List<Crew> _models = new List<Crew>();

    List<UnityPool> _poolByType = new List<UnityPool>();

    void Awake()
    {
        ConstructPools();
    }

    void ConstructPools()
    {
        _models.Sort((a, b) => a.type.CompareTo(b.type));

        for(int i = 0; i < _models.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.parent = transform;

            UnityPool pool = go.AddComponent<UnityPool>();
            pool.Construct(_models[i], _expand);
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
}
