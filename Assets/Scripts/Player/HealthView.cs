using UnityEngine;
using UnityEngine.UI;
using MagicTween;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    [SerializeField] private float _tweenDuration = 0.5f;

    public void SetHealth(float value)
    {
        Tween.To(
            () => _healthSlider.value,
            x => _healthSlider.value = x,
            value,
            _tweenDuration
        );
    }

    public void Initialize(float value)
    {
        _healthSlider.value = value;
    }
}
