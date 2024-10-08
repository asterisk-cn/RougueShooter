using R3;
using ObservableCollections;
using VContainer.Unity;
using Players;

public class PlayerDispatcher : IPostInitializable
{
    private PlayerProvider _playerProvider;
    private PlayerPresenter _playerPresenter;

    public PlayerDispatcher(PlayerProvider playerProvider, PlayerPresenter playerPresenter)
    {
        _playerProvider = playerProvider;
        _playerPresenter = playerPresenter;
    }

    public void PostInitialize()
    {
        // TODO: PlayerCore を全て生成するように変更
        foreach (var p in _playerProvider.Players)
        {
            Dispatch(p);
        }

        _playerProvider.Players
            .ObserveAdd()
            .Subscribe(x => Dispatch(x.Value));
    }

    void Dispatch(PlayerCore player)
    {
        var healthView = player.GetComponentInChildren<HealthView>();
        var health = player.GetComponent<Health>();
        _playerPresenter.BindHealth(health, healthView);
    }
}
