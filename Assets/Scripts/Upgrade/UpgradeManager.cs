using UnityEngine;
using System.Collections.Generic;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] UpgradeLoader _upgradeLoader;
        [SerializeField] Players.PlayerStatusManager _playerStatusManager;
        [SerializeField] Weapons.WeaponManager _weaponManager;
        [SerializeField] List<UpgradeId> _upgradeIdList;

        Players.PlayerParams _playerParamsVariation;
        List<GameObject> _weaponList;

        [SerializeField] List<GameObject> _weaponPrefabs;

        public Players.PlayerParams playerParamsVariation
        {
            get { return _playerParamsVariation; }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ApplyAllUpgrades();
        }

        public void ApplyAllUpgrades()
        {
            ApplyPlayerUpgrades();
            ApplyWeaponUpgrades();
        }

        void ApplyPlayerUpgrades()
        {
            _playerParamsVariation = new Players.PlayerParams();
            var playerUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeLoader.GetUpgrade(upgradeId) is PlayerUpgrade).ConvertAll(upgradeId => _upgradeLoader.GetUpgrade(upgradeId) as PlayerUpgrade);
            foreach (var playerUpgrade in playerUpgrades)
            {
                _playerParamsVariation += playerParamsVariation;
            }
            _playerStatusManager.ApplyPlayerParamsVariation(_playerParamsVariation);
        }

        void ApplyWeaponUpgrades()
        {
            _weaponList = new List<GameObject>();
            var addWeaponUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeLoader.GetUpgrade(upgradeId) is AddWeaponUpgrade).ConvertAll(upgradeId => _upgradeLoader.GetUpgrade(upgradeId) as AddWeaponUpgrade);
            foreach (var addWeaponUpgrade in addWeaponUpgrades)
            {
                var _weaponParamsVariation = new Weapons.WeaponParams();
                var weaponUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeLoader.GetUpgrade(upgradeId) is WeaponUpgrade && (_upgradeLoader.GetUpgrade(upgradeId) as WeaponUpgrade).weaponId == addWeaponUpgrade.weaponId).ConvertAll(upgradeId => _upgradeLoader.GetUpgrade(upgradeId) as WeaponUpgrade);
                foreach (var weaponUpgrade in weaponUpgrades)
                {
                    _weaponParamsVariation += weaponUpgrade.weaponParamsVariation;
                }
                var weapon = _weaponPrefabs[(int)addWeaponUpgrade.weaponId].GetComponent<Weapons.BasicGun>();
                weapon.ApplyWeaponParamsVariation(_weaponParamsVariation);
                _weaponList.Add(_weaponPrefabs[(int)addWeaponUpgrade.weaponId]);
            }
            _weaponManager.CreateWeapons(_weaponList);
        }

        public void AddUpgrade(UpgradeId upgradeId)
        {
            _upgradeIdList.Add(upgradeId);
        }
    }
}
