using UnityEngine;
using UniRx;
using Players;
using Cysharp.Threading.Tasks;

public class PlayerPresenter : MonoBehaviour
{
    public async void OnCreatePlayer(PlayerCore player, HealthView view)
    {
        var health = player.GetComponentInChildren<Health>();

        await health.InitializedAsync;

        Debug.Log("health: " + health.MaxHealth);
        view.Initialize((float)health.CurrentHealth.Value / health.MaxHealth);

        health.CurrentHealth
            .Subscribe(x => view.SetHealth((float)x / health.MaxHealth))
            .AddTo(this);
    }
}
