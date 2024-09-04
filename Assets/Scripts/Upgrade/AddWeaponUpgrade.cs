using UnityEngine;

namespace Upgrades
{
    public class AddWeaponUpgrade : IUpgrade
    {
        string _upgradeName;
        GameObject _weaponPrefab;

        public GameObject weaponPrefab
        {
            get { return _weaponPrefab; }
        }

        public void Initialize(UpgradeData upgradeData)
        {
            _upgradeName = upgradeData.upgradeName;
            _weaponPrefab = upgradeData.weaponPrefab;
        }
    }
}
