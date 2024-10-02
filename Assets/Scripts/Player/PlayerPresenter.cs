using UnityEngine;
using R3;
using Players;
using Cysharp.Threading.Tasks;

public class PlayerPresenter : MonoBehaviour
{
    public async void OnCreatePlayer(PlayerCore player, HealthView view)
    {
        var health = player.GetComponentInChildren<Health>();

        await health.InitializedAsync;

        view.Initialize((float)health.CurrentHealth.CurrentValue / health.MaxHealth);

        health.CurrentHealth
            .Subscribe(x => view.SetHealth((float)x / health.MaxHealth))
            .AddTo(this);
    }
}
