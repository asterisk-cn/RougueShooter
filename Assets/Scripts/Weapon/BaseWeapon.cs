using UnityEngine;
using Players;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        private protected PlayerCore _owner;
        private protected PlayerInput _input;

        private protected bool _isExecuting = false;

        private protected WeaponParams _defaultWeaponParams = new WeaponParams();
        private protected WeaponParams _currentWeaponParams = new WeaponParams();

        public void Initialize(PlayerCore owner, PlayerInput input)
        {
            _owner = owner;
            _input = input;

            ShootAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void ApplyWeaponParamsVariation(WeaponParams weaponParamsVariation)
        {
            _currentWeaponParams = _defaultWeaponParams + weaponParamsVariation;
        }

        private async UniTaskVoid ShootAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _isExecuting = _input.Attack.Value;

                if (_isExecuting)
                {
                    Execute();
                    await UniTask.Delay(TimeSpan.FromSeconds(_currentWeaponParams.fireRate), cancellationToken: ct);
                }
                else
                {
                    await UniTask.Yield();
                }
            }
        }

        private protected abstract void Execute();
    }
}
