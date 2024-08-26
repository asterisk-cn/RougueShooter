using UnityEngine;
using UniRx;

public class Health : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;

    public float MaxHealth { get; private set; }

    private ReactiveProperty<float> _currentHealth = new ReactiveProperty<float>();

    public void Initialize(float maxHealth)
    {
        this.MaxHealth = maxHealth;
        _currentHealth.Value = maxHealth;
    }

    public void Die()
    {
        Debug.Log("Die");
    }

    public void TakeDamage(float damage)
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
