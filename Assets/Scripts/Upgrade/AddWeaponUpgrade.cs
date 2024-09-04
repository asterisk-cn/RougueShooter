using UnityEngine;

namespace Upgrades
{
    public class AddWeaponUpgrade : IUpgrade
    {
        string _upgradeName;
        Weapons.WeaponId _weaponId;

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
