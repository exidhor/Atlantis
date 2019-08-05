using UnityEngine;
using System.Collections.Generic;

public class MonsterZone : MonoBehaviour
{
    [SerializeField] float _radius;
    [SerializeField] int _maxMonster;
    [SerializeField] MonsterType _type;
    [SerializeField] float _rebornTime;
    [SerializeField] List<Transform> _spawnPoints = new List<Transform>();

    List<Monster> _monsters = new List<Monster>();

    float _currentRebornTime;

    void Awake()
    {
        for(int i = 0; i < _maxMonster; i++)
        {
            Spawn();
        }
    }

    void Update()
    {
        if (_monsters.Count < _maxMonster)
        {
            _currentRebornTime += Time.deltaTime;

            if(_currentRebornTime > _rebornTime)
            {
                Spawn();
                _currentRebornTime -= _rebornTime;
            }
        }
    }

    void Spawn()
    {
        Monster monster = MonsterLibrary.instance.GetFreeMonster(_type);
        monster.SetZone(this);

        int spawnIndex = Random.Range(0, _spawnPoints.Count);
        monster.transform.position = _spawnPoints[spawnIndex].position;
        monster.transform.rotation = _spawnPoints[spawnIndex].rotation;

        _monsters.Add(monster);
    }
}
