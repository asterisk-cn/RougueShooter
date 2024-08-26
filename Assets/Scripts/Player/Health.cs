using UnityEngine;
using UniRx;

public class Health : MonoBehaviour
{
    private IDamageable _damageable;

    public IReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;

    public float MaxHealth { get; private set; }

    private ReactiveProperty<float> _currentHealth = new ReactiveProperty<float>();

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
    }

    void Die()
    {
        _damageable.OnDead();
        Debug.Log("Player is dead.");
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
