using UnityEngine;
using UnityEngine.InputSystem;
using R3;

namespace Players
{
    public class PlayerInput : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<Vector2> Move => _move;
        public ReadOnlyReactiveProperty<bool> Attack => _attack;

        private readonly ReactiveProperty<Vector2> _move = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<bool> _attack = new ReactiveProperty<bool>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _move.Value = Vector2.zero;
            _attack.Value = false;

            _move.AddTo(this);
            _attack.AddTo(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _move.Value = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _attack.Value = true;
            }
            else if (context.canceled)
            {
                _attack.Value = false;
            }
        }
    }
}

