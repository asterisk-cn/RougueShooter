using Players;
using UnityEngine;

namespace Weapons.Bullets
{
    public class HomingBullet : BaseBullet
    {
        private Transform _target;
        private float _homingStrength = 3f;
        private float _homingAngle = 90f;

        void Start()
        {
            FindTarget();
        }

        private protected override void Move()
        {
            if (_target != null)
            {
                Vector2 direction = (Vector2)_target.position - _rb.position;
                direction.Normalize();
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                float currentAngle = _rb.rotation;
                float deltaAngle = Mathf.DeltaAngle(currentAngle, targetAngle);

                if (Mathf.Abs(deltaAngle) < _homingAngle)
                {
                    float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * _homingStrength);
                    _rb.rotation = newAngle;
                }
                else
                {
                    _target = null;
                }
            }

            _rb.linearVelocity = transform.up * _bulletParams.speed;
        }

        void FindTarget()
        {
            var players = FindObjectsByType<PlayerCore>(FindObjectsSortMode.None);
            float distance = float.MaxValue;
            if (players.Length > 0)
            {
                foreach (var player in players)
                {
                    if (player.gameObject == _owner.gameObject)
                    {
                        continue;
                    }
                    float newDistance = Vector2.Distance(transform.position, player.transform.position);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        _target = player.transform;
                    }
                }
            }
        }
    }
}
