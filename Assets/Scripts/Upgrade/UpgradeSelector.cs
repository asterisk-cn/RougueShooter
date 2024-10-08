using UnityEngine;
using R3;
using System;
using System.Collections.Generic;
using VContainer;

namespace Upgrades
{
    public class UpgradeSelector : MonoBehaviour
    {
        [SerializeField] private UpgradeCollection _upgradeCollection;
        [SerializeField] private Battle _battle;
        [SerializeField] private Players.PlayerProvider _playerProvider;

        public Observable<List<IUpgrade>> OnUpgradesUpdatedAsObservable => _onUpgradesUpdated;
        public ReadOnlyReactiveProperty<bool> IsActive => _isActive;

        private Subject<List<IUpgrade>> _onUpgradesUpdated { get; } = new Subject<List<IUpgrade>>();
        private List<IUpgrade> _upgrades { get; } = new List<IUpgrade>();
        private ReactiveProperty<bool> _isActive { get; } = new ReactiveProperty<bool>(false);

        [Inject]
        public void Construct(Battle battle, UpgradeCollection upgradeCollection, Players.PlayerProvider playerProvider)
        {
            _battle = battle;
            _upgradeCollection = upgradeCollection;
            _playerProvider = playerProvider;

            _battle.CurrentState
                .Where(s => s == BattleState.Upgrade)
                .Subscribe(_ => OnUpgrade())
                .AddTo(this);

            _onUpgradesUpdated.AddTo(this);
            _isActive.AddTo(this);
        }

        void OnUpgrade()
        {
            Debug.Log("Upgrade phase started.");
            SetUpgrades();
            _isActive.Value = true;
        }

        public void SetUpgrades()
        {
            _upgrades.Clear();
            var upgrades = SelectUpgradeChoices(3);
            _upgrades.AddRange(upgrades);
            _onUpgradesUpdated.OnNext(_upgrades);
        }

        List<IUpgrade> SelectUpgradeChoices(int num)
        {
            var count = _upgradeCollection.GetUpgradeCount();
            var ids = Utility.GetUniqueRandomNumbers(0, count - 1, num);
            var idList = new List<int>(ids);
            return idList.ConvertAll(id => _upgradeCollection.GetUpgrade((UpgradeId)id));
        }

        public void ApplyUpgrade(int index)
        {
            Debug.Log($"Upgrade {index} applied to player {_battle.UpgradablePlayerId}.");
            var player = _playerProvider.Players[_battle.UpgradablePlayerId];
            var upgradeManager = player.GetComponent<UpgradeManager>();
            upgradeManager.AddUpgrade((UpgradeId)index);
            upgradeManager.ApplyAllUpgrades();

            _isActive.Value = false;
            _battle.SetState(BattleState.Battle);
        }
    }
}
