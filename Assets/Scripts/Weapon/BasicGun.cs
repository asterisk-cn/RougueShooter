using UnityEngine;
using System.Collections.Generic;
using Weapons.Bullets;

namespace Weapons
{
    public class BasicGun : BaseWeapon
    {
        [SerializeField] private BaseBullet _bulletPrefab;
        [SerializeField] private List<Transform> _firePoint;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        private protected override void Execute()
        {
            foreach (var firePoint in _firePoint)
            {
                var bullet = Instantiate(_bulletPrefab, firePoint.position, firePoint.rotation, this.transform);
                bullet.Initialize(_currentWeaponParams.bulletParams, _owner);
            }
        }
    }
}
