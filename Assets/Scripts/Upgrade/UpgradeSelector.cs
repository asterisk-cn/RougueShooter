using UnityEngine;
using R3;
using System;
using System.Collections.Generic;

namespace Upgrades
{
    public class UpgradeSelector : MonoBehaviour
    {
        [SerializeField] private UpgradeLoader _upgradeLoader;
        [SerializeField] private Battle _battle;
        [SerializeField] private Players.PlayerManager _playerManager;

        public Observable<List<IUpgrade>> OnUpgradesUpdatedAsObservable => _onUpgradesUpdated;
        public ReadOnlyReactiveProperty<bool> IsActive => _isActive;

        private Subject<List<IUpgrade>> _onUpgradesUpdated { get; } = new Subject<List<IUpgrade>>();
        private List<IUpgrade> _upgrades { get; } = new List<IUpgrade>();
        private ReactiveProperty<bool> _isActive { get; } = new ReactiveProperty<bool>(false);

        void Awake()
        {
            _onUpgradesUpdated.AddTo(this);
            _isActive.AddTo(this);
        }

        public void Initialize()
        {
            _battle.State
                .Where(s => s == Battle.BattleState.Upgrade)
                .Subscribe(_ => OnUpgrade())
                .AddTo(this);
        }

        void OnUpgrade()
        {
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
            var count = _upgradeLoader.GetUpgradeCount();
            var ids = Utility.GetUniqueRandomNumbers(0, count - 1, num);
            var idList = new List<int>(ids);
            return idList.ConvertAll(id => _upgradeLoader.GetUpgrade((UpgradeId)id));
        }

        public void ApplyUpgrade(int index)
        {
            Debug.Log($"Upgrade {index} applied to player {_battle.UpgradablePlayerId}.");
            var player = _playerManager.Players[_battle.UpgradablePlayerId];
            var upgradeManager = player.GetComponent<UpgradeManager>();
            upgradeManager.AddUpgrade((UpgradeId)index);
            upgradeManager.ApplyAllUpgrades();

            _isActive.Value = false;
            _battle.SetState(Battle.BattleState.Battle);
        }
    }
}
