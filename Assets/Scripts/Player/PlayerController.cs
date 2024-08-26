using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed;
    private float acceleration = 10f;
    private float rotationSpeed = 10f;

    private Vector2 moveDirection;
    private bool isMoving;
    private bool isFiring;
    private bool canShoot = true;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float fireRate = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Initialize(float speed)
    {
        this.speed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 targetVelocity = moveDirection * speed;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.deltaTime);
            
            // Rotate the player to the direction of movement
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            float rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rotation);
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, acceleration * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        if (context.started)
        {
            isMoving = true;
        }
        else if (context.performed)
        {
            isMoving = moveDirection != Vector2.zero;
        }
        else if (context.canceled)
        {
            isMoving = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireContinuously(this.GetCancellationTokenOnDestroy()).Forget();
        }
        else if (context.canceled)
        {
            isFiring = false;
        }
    }

    //TODO: Startで呼び出すように変更？その際elseで1フレーム待機するように変更？
    async UniTaskVoid FireContinuously(CancellationToken token)
    {
        isFiring = true;
        while (isFiring && canShoot)
        {
            FireBullet();
            canShoot = false;
            await UniTask.Delay((int)(fireRate * 1000));
            canShoot = true;
        }
    }

    private void FireBullet()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Initialize(gameObject);
    }
}
