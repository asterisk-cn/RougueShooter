using UnityEngine;
using VContainer;
using VContainer.Unity;
using Players;
using Upgrades;
using Managers;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private Battle _battle;
    [SerializeField] private PlayerProvider _playerProvider;
    [SerializeField] private UpgradeSelector _upgradeSelector;
    [SerializeField] private UpgradeSelectorView _upgradeSelectorView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerDispatcher>(Lifetime.Singleton);
        builder.RegisterEntryPoint<UpgradeSelectorPresenter>(Lifetime.Singleton);

        builder.Register<PlayerPresenter>(Lifetime.Singleton);
        builder.Register<UpgradeCollection>(Lifetime.Singleton);

        builder.RegisterComponent(_battle);
        builder.RegisterComponent(_playerProvider);
        builder.RegisterComponent(_upgradeSelector);
        builder.RegisterComponent(_upgradeSelectorView);
    }
}
