using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Health : MonoBehaviour
{
    private IDamageable _damageable;

    public IReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;
    public UniTask InitializedAsync => _initializationCompletionSource.Task;
    public float MaxHealth { get; private set; }

    private ReactiveProperty<float> _currentHealth = new ReactiveProperty<float>();
    private readonly UniTaskCompletionSource _initializationCompletionSource = new UniTaskCompletionSource();

    void Awake()
    {
        _damageable = GetComponentInParent<IDamageable>();
    }

    void Start()
    {

    }

    public void Initialize(float maxHealth)
    {
        this.MaxHealth = maxHealth;
        _currentHealth.Value = maxHealth;

        _damageable.OnDamagedAsObservable
            .Where(x => x > 0)
            .Subscribe(x => TakeDamage(x))
            .AddTo(this);

        _initializationCompletionSource.TrySetResult();
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

    private void OnDestroy()
    {
        _currentHealth.Dispose();
    }
}
