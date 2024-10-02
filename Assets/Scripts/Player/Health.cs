using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;

public class Health : MonoBehaviour
{
    private IDamageable _damageable;

    public ReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;
    public UniTask InitializedAsync => _initializationCompletionSource.Task;
    public float MaxHealth { get; private set; }

    private ReactiveProperty<float> _currentHealth = new ReactiveProperty<float>();
    private readonly UniTaskCompletionSource _initializationCompletionSource = new UniTaskCompletionSource();

    void Awake()
    {
        _currentHealth.AddTo(this);

        _damageable = GetComponentInParent<IDamageable>();
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
        this.MaxHealth = maxHealth;
        _currentHealth.Value = maxHealth;
    }

    void Die()
    {
        _damageable.OnDead();
    }

    void TakeDamage(float damage)
    {
        _currentHealth.Value = Mathf.Clamp(_currentHealth.Value - damage, 0, MaxHealth);

        if (_currentHealth.Value <= 0)
        {
            Die();
        }
    }
}
