using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] BulletType _bulletType;
    [SerializeField] int _damage;
    [SerializeField] float _speed;

    public void Fire(ITargetable target)
    {
        Bullet b = BulletLibrary.instance.GetFreeBullet(_bulletType);

        b.Init(transform.position,
               target,
               _damage,
               _speed);
    }
}
