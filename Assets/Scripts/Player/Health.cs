using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;

namespace Players
{
    public class Health : MonoBehaviour
    {
        private IDamageable _damageable;

        public Observable<Unit> OnCurrentHealthChangedAsObservable => _onCurrentHealthChanged;
        public Observable<Unit> OnCurrentHealthChangedWithoutAnimationAsObservable => _onCurrentHealthChangedWithoutAnimation;
        public UniTask InitializedAsync => _initializationCompletionSource.Task;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        private float _maxHealth;
        private float _currentHealth;
        private Subject<Unit> _onCurrentHealthChanged = new Subject<Unit>();
        private Subject<Unit> _onCurrentHealthChangedWithoutAnimation = new Subject<Unit>();
        private readonly UniTaskCompletionSource _initializationCompletionSource = new UniTaskCompletionSource();

        void Awake()
        {
            _damageable = GetComponentInParent<IDamageable>();

            _onCurrentHealthChanged.AddTo(this);
            _onCurrentHealthChangedWithoutAnimation.AddTo(this);
        }

        public void Initialize(float maxHealth)
        {
            _damageable.OnDamagedAsObservable
                .Where(x => x > 0)
                .Subscribe(x => TakeDamage(x))
                .AddTo(this);

            _damageable.OnResetAsObservable
                .Subscribe(_ => ResetHealth(maxHealth))
                .AddTo(this);

            ResetHealth(maxHealth);

            _initializationCompletionSource.TrySetResult();
        }

        public void ResetHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;

            _onCurrentHealthChangedWithoutAnimation.OnNext(Unit.Default);
        }

        void Die()
        {
            _damageable.OnDead();
        }

        void TakeDamage(float damage)
        {
            if (_currentHealth <= 0)
            {
                return;
            }
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, MaxHealth);
            _onCurrentHealthChanged.OnNext(Unit.Default);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }
}
