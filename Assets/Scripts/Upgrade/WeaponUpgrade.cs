using UnityEngine;

namespace Upgrades
{
    public class WeaponUpgrade : IUpgrade
    {
        private string _upgradeName;
        private Weapons.WeaponId _weaponId;
        private Weapons.WeaponParams _weaponParamsVariation;

        public string upgradeName
        {
            get { return _upgradeName; }
        }

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
