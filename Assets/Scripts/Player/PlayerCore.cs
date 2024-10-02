using System;
using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;

namespace Players
{
    public class PlayerCore : MonoBehaviour, IDamageable
    {
        public Observable<float> OnDamagedAsObservable => _onDamaged;
        public Observable<Unit> PlayerDeadAsync => _playerDeadSubject;
        public Observable<Unit> OnResetAsObservable => _onReset;

        private Subject<float> _onDamaged = new Subject<float>();
        private Subject<Unit> _playerDeadSubject = new Subject<Unit>();
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

            gameObject.SetActive(false);
        }

        public void Reset()
        {
            _onReset.OnNext(Unit.Default);

            gameObject.SetActive(true);
        }
    }
}
