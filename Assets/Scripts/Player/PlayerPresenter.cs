using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    public void OnCreatePlayer(GameObject player, HealthView view)
    {
        var health = player.GetComponentInChildren<Health>();

        view.Initialize((float)health.CurrentHealth.Value / health.MaxHealth);

        health.CurrentHealth
            .Subscribe(x => view.SetHealth((float)x / health.MaxHealth))
            .AddTo(this);
    }
}
