using UnityEngine;
using R3;
using ObservableCollections;
using Players;

public class PlayerDispatcher : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerPresenter _playerPresenter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var p in _playerManager.Players)
        {
            Dispatch(p);
        }

        _playerManager.Players
            .ObserveAdd()
            .Subscribe(x => Dispatch(x.Value))
            .AddTo(this);
    }

    void Dispatch(PlayerCore player)
    {
        var healthView = player.GetComponentInChildren<HealthView>();
        _playerPresenter.OnCreatePlayer(player, healthView);
    }
}
