using UnityEngine;
using System.Collections.Generic;
using Tools;
using MemoryManagement;
using System;

public class Library<This, Info, Model> : MonoSingleton<This>
    where This : Library<This, Info, Model>
    where Info : LibraryInfo<Model>
    where Model : UnityPoolObject, ILibraryModel
{
    [Header("Pool Infos")]
    [SerializeField] uint _poolCapacity;
    [SerializeField] uint _expand;

    [Header("Infos")]
    [SerializeField] 
    List<LibraryInfo<Model>> _infos = new List<LibraryInfo<Model>>();

    List<UnityPool> _poolByType = new List<UnityPool>();

    void Awake()
    {
        ConstructPools();
    }

    void ConstructPools()
    {
        _infos.Sort((a, b) => a.model.enumValue.CompareTo(b.model.enumValue));

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

    public Model GetFreeObject(int enumValue)
    {
        Model obj = (Model)_poolByType[enumValue].GetFreeResource();

        obj.transform.localPosition = Vector3.zero;

        return obj;
    }

    public LibraryInfo<Model> GetInfo(int enumValue)
    {
        return _infos[enumValue];
    }
}
