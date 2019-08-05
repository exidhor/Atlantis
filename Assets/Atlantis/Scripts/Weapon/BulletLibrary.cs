using UnityEngine;
using System.Collections.Generic;
using Tools;
using MemoryManagement;
using System;

public class BulletLibrary : MonoSingleton<BulletLibrary>
{
    [Header("Pool Infos")]
    [SerializeField] uint _poolCapacity;
    [SerializeField] uint _expand;

    [Header("Infos")]
    [SerializeField] List<BulletInfo> _infos = new List<BulletInfo>();

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

    public Bullet GetFreeBullet(BulletType type)
    {
        Bullet bullet = (Bullet)_poolByType[(int)type].GetFreeResource();

        bullet.transform.localPosition = Vector3.zero;

        return bullet;
    }

    public BulletInfo GetInfo(BulletType type)
    {
        return _infos[(int)type];
    }
}
