using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;
using VContainer.Unity;


namespace Players
{
    public class Health
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


        public Health(IDamageable damageable)
        {
            if (damageable == null)
            {
                throw new System.ArgumentNullException(nameof(damageable));
            }

            if (damageable.MaxHealth <= 0)
            {
                throw new System.ArgumentException("MaxHealth must be greater than 0", nameof(damageable.MaxHealth));
            }

            _damageable = damageable;

            PostInitialize();
        }

        private void PostInitialize()
        {
            ResetHealth(_damageable.MaxHealth);

            _damageable.OnDamagedAsObservable
                .Where(x => x > 0)
                .Subscribe(x => TakeDamage(x));

            _damageable.OnResetAsObservable
                .Subscribe(_ => ResetHealth(_maxHealth));

            _initializationCompletionSource.TrySetResult();
        }

        public void ResetHealth(float maxHealth)
        {
            if (maxHealth <= 0)
            {
                throw new System.ArgumentException("MaxHealth must be greater than 0", nameof(maxHealth));
            }

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
            Debug.Log(damage);
            if (damage <= 0)
            {
                throw new System.ArgumentException("Damage must be greater than 0", nameof(damage));
            }

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
