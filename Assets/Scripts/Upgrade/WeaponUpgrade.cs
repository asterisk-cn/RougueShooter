using UnityEngine;

namespace Upgrades
{
    public class WeaponUpgrade : IUpgrade
    {
        string _upgradeName;
        Weapons.WeaponId _weaponId;
        Weapons.WeaponParams _weaponParamsVariation;

        public Weapons.WeaponId weaponId
        {
            get { return _weaponId; }
        }

        public Weapons.WeaponParams weaponParamsVariation
        {
            get { return _weaponParamsVariation; }
        }

        public void Initialize(UpgradeData upgradeData)
        {
            _upgradeName = upgradeData.upgradeName;
            _weaponId = upgradeData.weaponId;
            _weaponParamsVariation = upgradeData.weaponParamsVariation;
        }
    }
}
