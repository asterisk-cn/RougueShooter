using UnityEngine;
using System.Collections.Generic;
using UniRx;
using Shapes2D;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] Players.PlayerStatusManager _playerStatusManager;
        [SerializeField] Weapons.WeaponManager _weaponManager;
        [SerializeField] List<UpgradeId> _upgradeIdList;

        List<IUpgrade> _upgradeList;
        Players.PlayerParams _playerParamsVariation;
        List<GameObject> _weaponList;

        [SerializeField] List<GameObject> _weaponPrefabs;

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
            ApplyPlayerUpgrades();
            ApplyWeaponUpgrades();
        }

        void ApplyPlayerUpgrades()
        {
            _playerParamsVariation = new Players.PlayerParams();
            var playerUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeList[(int)upgradeId] is PlayerUpgrade).ConvertAll(upgradeId => _upgradeList[(int)upgradeId] as PlayerUpgrade);
            foreach (var playerUpgrade in playerUpgrades)
            {
                _playerParamsVariation += playerParamsVariation;
            }
            _playerStatusManager.ApplyPlayerParamsVariation(_playerParamsVariation);
        }

        void ApplyWeaponUpgrades()
        {
            _weaponList = new List<GameObject>();
            var addWeaponUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeList[(int)upgradeId] is AddWeaponUpgrade).ConvertAll(upgradeId => _upgradeList[(int)upgradeId] as AddWeaponUpgrade);
            foreach (var addWeaponUpgrade in addWeaponUpgrades)
            {
                var _weaponParamsVariation = new Weapons.WeaponParams();
                var weaponUpgrades = _upgradeList.FindAll(upgrade => upgrade is WeaponUpgrade && (upgrade as WeaponUpgrade).weaponId == addWeaponUpgrade.weaponId);
                foreach (var weaponUpgrade in weaponUpgrades)
                {
                    _weaponParamsVariation += (weaponUpgrade as WeaponUpgrade).weaponParamsVariation;
                }
                var weapon = _weaponPrefabs[(int)addWeaponUpgrade.weaponId].GetComponent<Weapons.BasicGun>();
                weapon.ApplyWeaponParamsVariation(_weaponParamsVariation);
                _weaponList.Add(_weaponPrefabs[(int)addWeaponUpgrade.weaponId]);
            }
            _weaponManager.CreateWeapons(_weaponList);
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
