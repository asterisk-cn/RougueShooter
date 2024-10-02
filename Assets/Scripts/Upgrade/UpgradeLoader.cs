using UnityEngine;
using System.Collections.Generic;

namespace Upgrades
{
    public class UpgradeLoader : MonoBehaviour
    {
        [SerializeField] private List<IUpgrade> _upgradeList;

        void Awake()
        {
            LoadUpgradeSetting();
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

        IUpgrade CreateUpgrade(UpgradeData upgradeData)
        {
            switch (upgradeData.upgradeType)
            {
                case UpgradeType.Player:
                    return new PlayerUpgrade();
                case UpgradeType.AddWeapon:
                    return new AddWeaponUpgrade();
                case UpgradeType.Weapon:
                    return new WeaponUpgrade();
                default:
                    throw new System.Exception("Invalid upgrade type: " + upgradeData.upgradeType);
            }
        }

        public IUpgrade GetUpgrade(UpgradeId upgradeId)
        {
            return _upgradeList[(int)upgradeId];
        }

        public int GetUpgradeCount()
        {
            return _upgradeList.Count;
        }
    }
}
