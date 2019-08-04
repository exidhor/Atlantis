using UnityEngine;
using System.Collections.Generic;
using Tools;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    List<Monster> _monsters = new List<Monster>();

    public void Register(Monster monster)
    {

    }

    public void Unregister(Monster monster)
    {

    }
}
