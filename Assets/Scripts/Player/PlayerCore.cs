using System;
using UnityEngine;
using UniRx;

namespace Players
{
    public class PlayerCore : MonoBehaviour, IDamageable
    {
        public IObservable<float> OnDamagedAsObservable => _onDamaged;

        private Subject<float> _onDamaged = new Subject<float>();

        private float maxHealth;
        private float speed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ReadPlayerPropertyDataAsset();

            GetComponentInChildren<Health>().Initialize(maxHealth);
            GetComponentInChildren<PlayerMove>().Initialize(speed);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(float damage)
        {
            _onDamaged.OnNext(damage);
        }

        void ReadPlayerPropertyDataAsset()
        {
            var path = "Data/PlayerPropertyData";
            var playerParameter = Resources.Load<PlayerPropertyAsset>(path);

            this.speed = playerParameter.speed;
            this.maxHealth = playerParameter.maxHealth;
        }
    }
}
