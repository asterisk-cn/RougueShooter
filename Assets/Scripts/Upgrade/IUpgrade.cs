using UnityEngine;

namespace Upgrades
{
    public interface IUpgrade
    {
        void Initialize(UpgradeData upgradeData);

        public string upgradeName { get; }
    }
}
