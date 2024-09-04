using UnityEngine;
using System.Collections.Generic;
using UniRx;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] Players.PlayerStatusManager _playerStatusManager;
        [SerializeField] List<UpgradeId> _upgradeIdList;

        List<IUpgrade> _upgradeList;
        Players.PlayerParams _playerParamsVariation;

        public Players.PlayerParams playerParamsVariation
        {
            get { return _playerParamsVariation; }
        }

        void Awake()
        {
            // TODO: static class に移行
            LoadUpgradeSetting();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ApplyAllUpgrades();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ApplyAllUpgrades()
        {
            _playerParamsVariation = new Players.PlayerParams();

            // TODO: タイプごとに分ける
            foreach (var upgradeId in _upgradeIdList)
            {
                ApplyUpgrade(upgradeId);
            }

            _playerStatusManager.ApplyPlayerParamsVariation(_playerParamsVariation);
        }

        void ApplyUpgrade(UpgradeId upgradeId)
        {
            IUpgrade upgrade = _upgradeList[(int)upgradeId];
            if (upgrade == null)
            {
                Debug.LogError("Upgrade not found: " + upgradeId);
                return;
            }

            if (upgrade is PlayerUpgrade)
            {
                _playerParamsVariation += (upgrade as PlayerUpgrade).playerParamsVariation;
            }
            else if (upgrade is AddWeaponUpgrade)
            {
                // Add weapon to player
            }
            else if (upgrade is WeaponUpgrade)
            {
                // Upgrade weapon
            }

        }

        IUpgrade CreateUpgrade(UpgradeData upgradeData)
        {
            switch (upgradeData.upgradeType)
            {
                case UpgradeType.Player:
                    return new PlayerUpgrade();
                case UpgradeType.AddWeapon:
                    return new AddWeaponUpgrade();
                // case UpgradeType.Weapon:
                //     return new WeaponUpgrade();
                default:
                    throw new System.Exception("Invalid upgrade type: " + upgradeData.upgradeType);
            }
        }

        void LoadUpgradeSetting()
        {
            var upgradeSetting = Resources.Load<UpgradeSetting>("Data/UpgradeSetting");
            _upgradeList = new List<IUpgrade>();
            foreach (var upgrade in upgradeSetting.upgradeDataList)
            {
                IUpgrade upgradeInstance = CreateUpgrade(upgrade);
                upgradeInstance.Initialize(upgrade);
                _upgradeList.Add(upgradeInstance);
            }
        }
    }
}
