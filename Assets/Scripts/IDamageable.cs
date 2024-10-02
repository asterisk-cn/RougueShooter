using System;
using UnityEngine;
using UniRx;

public interface IDamageable
{
    public IObservable<float> OnDamagedAsObservable { get; }
    public IObservable<Unit> OnResetAsObservable { get; }

    public void TakeDamage(float damage);
    public void OnDead();
}
