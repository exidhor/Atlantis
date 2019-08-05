using UnityEngine;
using System.Collections.Generic;
using Tools;
using MemoryManagement;

public class MonsterLibrary : MonoSingleton<MonsterLibrary>
{
    [Header("Pool Infos")]
    [SerializeField] uint _poolCapacity;
    [SerializeField] uint _expand;

    [Header("Infos")]
    [SerializeField] List<MonsterInfo> _infos = new List<MonsterInfo>();

    List<UnityPool> _poolByType = new List<UnityPool>();

    void Awake()
    {
        ConstructPools();
    }

    void ConstructPools()
    {
        _infos.Sort((a, b) => a.model.type.CompareTo(b.model.type));

        for (int i = 0; i < _infos.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.parent = transform;

            UnityPool pool = go.AddComponent<UnityPool>();
            pool.Construct(_infos[i].model, _expand);
            pool.SetSize(_poolCapacity);

            _poolByType.Add(pool);
        }
    }

    public Monster GetFreeMonster(MonsterType type)
    {
        Monster monster = (Monster)_poolByType[(int)type].GetFreeResource();

        monster.transform.localPosition = Vector3.zero;

        return monster;
    }

    public MonsterInfo GetInfo(MonsterType type)
    {
        return _infos[(int)type];
    }
}
