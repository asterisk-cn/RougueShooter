using UnityEngine;
using Weapon.Bullets;

namespace Weapons.Bullets
{
    public class BasicBullet : MonoBehaviour
    {
        private BulletParams bulletParams;
        private Rigidbody2D rb;

        private float speed = 15f;
        private float damage = 10f;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            rb.linearVelocity = transform.up * bulletParams.speed;
        }

        public void Initialize(GameObject owner)
        {
            bulletParams = new BulletParams();
            bulletParams.owner = owner;

            bulletParams.speed = speed;
            bulletParams.damage = damage;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != bulletParams.owner)
            {
                if (other.transform.root.TryGetComponent(out IDamageable _damageable))
                {
                    _damageable.TakeDamage(bulletParams.damage);
                }

                Destroy(gameObject);
            }
        }
    }
}
