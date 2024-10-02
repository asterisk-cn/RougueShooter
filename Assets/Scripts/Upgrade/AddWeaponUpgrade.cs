using UnityEngine;

namespace Upgrades
{
    public class AddWeaponUpgrade : IUpgrade
    {
        private string _upgradeName;
        private Weapons.WeaponId _weaponId;

        public string upgradeName
        {
            get { return _upgradeName; }
        }

        public Weapons.WeaponId weaponId
        {
            get { return _weaponId; }
        }

        public void Initialize(UpgradeData upgradeData)
        {
            _upgradeName = upgradeData.upgradeName;
            _weaponId = upgradeData.weaponId;
        }
    }
}
