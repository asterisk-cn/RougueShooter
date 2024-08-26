using System;
using UnityEngine;

[Serializable]
public struct BulletProperties
{
    public GameObject owner;
    public float speed;
    public float damage;
}

public class Bullet : MonoBehaviour
{
    private BulletProperties properties = new BulletProperties();
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
        rb.linearVelocity = transform.up * properties.speed;
    }

    public void Initialize(GameObject owner)
    {
        properties.owner = owner;

        properties.speed = speed;
        properties.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != properties.owner)
        {
            if (other.transform.root.TryGetComponent(out IDamageable _damageable))
            {
                _damageable.TakeDamage(properties.damage);
            }

            Destroy(gameObject);
        }
    }
}
