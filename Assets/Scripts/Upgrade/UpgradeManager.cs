using UnityEngine;
using System.Collections.Generic;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        UpgradeSetting _upgradeSetting;

        [SerializeField] List<UpgradeId> _upgradeIdList;
        List<UpgradeData> _upgradeDataList;
        Players.PlayerParams _playerParamsVariation;

        public Players.PlayerParams playerParamsVariation
        {
            get { return _playerParamsVariation; }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LoadUpgradeSetting();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ApplyAllUpgrades()
        {
            _upgradeDataList = new List<UpgradeData>();
        }

        void ApplyPlayerUpgrade()
        {
            _playerParamsVariation = new Players.PlayerParams();

            var playerUpgradeList = _upgradeDataList.FindAll(upgradeData => upgradeData.upgradeType == UpgradeType.Player);
            foreach (var playerUpgrade in playerUpgradeList)
            {
                var variation = playerUpgrade.playerParamsVariation;
                _playerParamsVariation.maxHealth += variation.maxHealth;
                _playerParamsVariation.speed += variation.speed;
            }
        }

        void UpdateUpgradeDataList()
        {
            _upgradeDataList = new List<UpgradeData>();
            foreach (var upgradeId in _upgradeIdList)
            {
                _upgradeDataList.Add(_upgradeSetting.GetUpgradeData(upgradeId));
            }
        }

        void LoadUpgradeSetting()
        {
            _upgradeSetting = Resources.Load<UpgradeSetting>("Data/UpgradeSetting");
        }
    }
}
