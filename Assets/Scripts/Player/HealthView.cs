using UnityEngine;
using UnityEngine.UI;
using MagicTween;

namespace Players
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        [SerializeField] private float _tweenDuration = 0.5f;

        //! TODO: 非同期処理
        public void SetHealth(float value)
        {
            Tween.To(
                () => _healthSlider.value,
                x => _healthSlider.value = x,
                value,
                _tweenDuration
            );
        }

        public void SetHealthWithoutAnimation(float value)
        {
            _healthSlider.value = value;
        }

        public void Initialize(float value)
        {
            _healthSlider.value = value;
        }
    }
}
