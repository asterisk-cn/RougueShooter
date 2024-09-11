using Players;
using UnityEngine;

namespace Weapons.Bullets
{
    public class BaseBullet : MonoBehaviour
    {
        private protected PlayerCore _owner;
        private protected BulletParams _bulletParams;
        private protected Rigidbody2D _rb;

        private protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(BulletParams bulletParams, PlayerCore owner)
        {
            _bulletParams = bulletParams;
            _owner = owner;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private protected virtual void Move()
        {
            _rb.linearVelocity = transform.up * _bulletParams.speed;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.root != _owner.transform)
            {
                if (other.transform.root.TryGetComponent(out IDamageable _damageable))
                {
                    _damageable.TakeDamage(_bulletParams.damage);
                }

                Destroy(gameObject);
            }
        }
    }
}
