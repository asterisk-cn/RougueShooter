using System;
using UnityEngine;
using UniRx;

namespace Players
{
    public class PlayerCore : MonoBehaviour, IDamageable
    {
        public IObservable<float> OnDamagedAsObservable => _onDamaged;
        public IObservable<Unit> PlayerDeadAsync => _playerDeadSubject;
        public IObservable<Unit> OnResetAsObservable => _onReset;

        private Subject<float> _onDamaged = new Subject<float>();
        private AsyncSubject<Unit> _playerDeadSubject = new AsyncSubject<Unit>();
        private Subject<Unit> _onReset = new Subject<Unit>();

        public PlayerParams PlayerParams => _playerParams;

        public int PlayerId { get; private set; }
        private PlayerParams _playerParams = new PlayerParams();

        void Awake()
        {
            _onDamaged.AddTo(this);
            _playerDeadSubject.AddTo(this);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // TODO: 依存逆にする?
            GetComponentInChildren<Health>().Initialize(_playerParams.maxHealth);
            GetComponentInChildren<PlayerMove>().Initialize(_playerParams.speed);
        }

        public void Initialize(int playerId)
        {
            this.PlayerId = playerId;
        }

        // TODO: PlayerStatusManagerを挟む
        public void SetPlayerParams(PlayerParams playerParams)
        {
            this._playerParams = playerParams;
        }

        public void TakeDamage(float damage)
        {
            _onDamaged.OnNext(damage);
        }

        public void OnDead()
        {
            _playerDeadSubject.OnNext(Unit.Default);
            _playerDeadSubject.OnCompleted();

            gameObject.SetActive(false);
            // Destroy(gameObject);
        }

        public void Reset()
        {
            _onReset.OnNext(Unit.Default);
            _playerDeadSubject = new AsyncSubject<Unit>();

            gameObject.SetActive(true);
        }
    }
}
