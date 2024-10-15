using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using Players;
using R3;

public class HealthTest
{
    Health _health;
    IDamageable _damageable;
    Subject<float> _damagedSubject;

    [SetUp]
    public void SetUp()
    {
        _damageable = Substitute.For<IDamageable>();
        _damageable.MaxHealth.Returns(100);
        _damagedSubject = new Subject<float>();
        _damageable.OnDamagedAsObservable.Returns(_damagedSubject);
        _damageable.OnResetAsObservable.Returns(new Subject<Unit>());

        _health = new Health(_damageable);
    }

    [Test]
    [Category("Constructor")]
    public void Construct()
    {
        Assert.That(_health, Is.Not.Null);

        Assert.That(_health.CurrentHealth, Is.EqualTo(100));
        Assert.That(_health.MaxHealth, Is.EqualTo(100));
    }

    [Test]
    [Category("Constructor")]
    public void ConstructWithNull()
    {
        Assert.That(() => new Health(null), Throws.ArgumentNullException);
    }

    [Test]
    [Category("Constructor")]
    public void ConstructWithUnderZero([Values(0, -1, -10)] float maxHealth)
    {
        _damageable.MaxHealth.Returns(maxHealth);
        Assert.That(() => new Health(_damageable), Throws.ArgumentException);
    }

    [Test]
    [Category("Damage")]
    public void TakeDamage([Values(10, 20, 30)] float damage)
    {
        _damagedSubject.OnNext(damage);
        Assert.That(_health.CurrentHealth, Is.EqualTo(100 - damage));
    }

    [Test]
    [Category("Damage")]
    public void UnderZeroDamage([Values(0, -1, -10)] float damage)
    {
        _damagedSubject.OnNext(damage);
        Assert.That(_health.CurrentHealth, Is.EqualTo(100));
    }

    [Test]
    [Category("Damage")]
    public void Die()
    {
        _damagedSubject.OnNext(100);
        Assert.That(_health.CurrentHealth, Is.EqualTo(0));
        _damageable.Received(1).OnDead();
    }

    [Test]
    [Category("Damage")]
    public void Overkill()
    {
        _damagedSubject.OnNext(200);
        Assert.That(_health.CurrentHealth, Is.EqualTo(0));
        _damageable.Received(1).OnDead();
    }

    [Test]
    [Category("Reset")]
    public void Reset()
    {
        _health.ResetHealth(150);
        Assert.That(_health.CurrentHealth, Is.EqualTo(150));
    }

    [Test]
    [Category("Reset")]
    public void ResetWithUnderZero([Values(0, -1, -10)] float maxHealth)
    {
        Assert.That(() => _health.ResetHealth(maxHealth), Throws.ArgumentException);
    }
}
