using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using UniRx.Triggers;

namespace Players
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        private Rigidbody2D _rb;

        private float _speed;
        private float _acceleration = 10f;
        private float _rotationSpeed = 10f;

        private bool _isMoving;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    Move(_input.Move.Value);
                }).AddTo(this);
        }

        public void Initialize(float speed)
        {
            this._speed = speed;
        }

        void Move(Vector2 value)
        {
            _isMoving = value != Vector2.zero;

            if (_isMoving)
            {
                Vector2 targetVelocity = value * _speed;
                _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, targetVelocity, _acceleration * Time.deltaTime);

                // Rotate the player to the direction of movement
                float angle = Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg - 90f;
                float rotation = Mathf.LerpAngle(_rb.rotation, angle, _rotationSpeed * Time.deltaTime);
                _rb.MoveRotation(rotation);
            }
            else
            {
                _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, Vector2.zero, _acceleration * Time.deltaTime);
            }
        }
    }
}
