using System;
using UnityEngine;
using R3;

public interface IDamageable
{
    public Observable<float> OnDamagedAsObservable { get; }
    public Observable<Unit> OnResetAsObservable { get; }

    public void TakeDamage(float damage);
    public void OnDead();
}
