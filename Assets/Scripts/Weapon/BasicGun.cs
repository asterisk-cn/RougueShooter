using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Players;
using System.Collections.Generic;

namespace Weapons
{
    public class BasicGun : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerCore _owner;

        private bool _isShooting = false;

        [SerializeField] private Bullets.BasicBullet _bulletPrefab;
        [SerializeField] private List<Transform> _firePoint;

        private WeaponParams _defaultWeaponParams = new WeaponParams();
        private WeaponParams _currentWeaponParams = new WeaponParams();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void Initialize(PlayerCore owner, PlayerInput input)
        {
            _owner = owner;
            _input = input;

            ShootAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        async UniTaskVoid ShootAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _isShooting = _input.Attack.Value;

                if (_isShooting)
                {
                    Shoot();
                    await UniTask.Delay(TimeSpan.FromSeconds(_currentWeaponParams.fireRate), cancellationToken: ct);
                }
                else
                {
                    await UniTask.Yield();
                }
            }
        }

        void Shoot()
        {
            foreach (var firePoint in _firePoint)
            {
                var bullet = Instantiate(_bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.Initialize(_currentWeaponParams.bulletParams, _owner);
            }
        }

        public void ApplyWeaponParamsVariation(WeaponParams weaponParamsVariation)
        {
            _currentWeaponParams = _defaultWeaponParams + weaponParamsVariation;
        }
    }
}
