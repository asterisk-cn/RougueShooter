using Players;
using UnityEngine;

namespace Weapons.Bullets
{
    public class BasicBullet : MonoBehaviour
    {
        private PlayerCore _owner;
        private BulletParams _bulletParams;
        private Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _rb.linearVelocity = transform.up * _bulletParams.speed;
        }

        public void Initialize(BulletParams bulletParams, PlayerCore owner)
        {
            _bulletParams = bulletParams;
            _owner = owner;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != _owner)
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
