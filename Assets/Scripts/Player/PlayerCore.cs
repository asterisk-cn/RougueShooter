using System;
using UnityEngine;
using UniRx;

namespace Players
{
    public class PlayerCore : MonoBehaviour, IDamageable
    {
        public IObservable<float> OnDamagedAsObservable => _onDamaged;
        public IObservable<Unit> PlayerDeadAsync => _playerDeadSubject;

        private Subject<float> _onDamaged = new Subject<float>();
        private readonly AsyncSubject<Unit> _playerDeadSubject = new AsyncSubject<Unit>();

        public int PlayerId { get; private set; }
        private float maxHealth;
        private float speed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ReadPlayerPropertyDataAsset();

            // TODO: 依存整理？
            GetComponentInChildren<Health>().Initialize(maxHealth);
            GetComponentInChildren<PlayerMove>().Initialize(speed);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(int playerId)
        {
            this.PlayerId = playerId;
        }

        public void TakeDamage(float damage)
        {
            _onDamaged.OnNext(damage);
        }

        public void OnDead()
        {
            _playerDeadSubject.OnNext(Unit.Default);
            _playerDeadSubject.OnCompleted();

            // _playerDeadSubject.Dispose();
        }

        void ReadPlayerPropertyDataAsset()
        {
            var path = "Data/PlayerSetting";
            var playerParams = Resources.Load<PlayerSetting>(path);

            this.speed = playerParams.speed;
            this.maxHealth = playerParams.maxHealth;
        }
    }
}
