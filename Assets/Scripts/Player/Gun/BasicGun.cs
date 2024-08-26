using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Players.Guns
{
    public class BasicGun : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;

        private bool _isShooting = false;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        private float _fireRate = 0.5f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
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
                    await UniTask.Delay(TimeSpan.FromSeconds(_fireRate), cancellationToken: ct);
                }
                else
                {
                    await UniTask.Yield();
                }
            }
        }

        void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.Initialize(gameObject);
        }
    }
}
