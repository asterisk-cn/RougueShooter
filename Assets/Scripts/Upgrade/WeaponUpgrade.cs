using UnityEngine;

namespace Upgrades
{
    public class WeaponUpgrade : IUpgrade
    {
        string _upgradeName;
        Weapons.WeaponParams _weaponParamsVariation;

        public Weapons.WeaponParams weaponParamsVariation
        {
            get { return _weaponParamsVariation; }
        }

        public void Initialize(UpgradeData upgradeData)
        {
            _upgradeName = upgradeData.upgradeName;
            _weaponParamsVariation = upgradeData.weaponParamsVariation;
        }
    }
}
