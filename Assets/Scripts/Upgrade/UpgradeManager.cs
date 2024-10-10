using UnityEngine;
using System.Collections.Generic;
using R3;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private Players.PlayerStatusManager _playerStatusManager;
        [SerializeField] private Weapons.WeaponManager _weaponManager;
        [SerializeField] private List<UpgradeId> _upgradeIdList = new List<UpgradeId>();

        private UpgradeCollection _upgradeCollection;

        private Players.PlayerParams _playerParamsVariation;
        private List<GameObject> _weaponList;

        [SerializeField] private List<GameObject> _weaponPrefabs;

        public Players.PlayerParams playerParamsVariation
        {
            get { return _playerParamsVariation; }
        }

        public void Initialize(UpgradeCollection upgradeCollection)
        {
            _upgradeCollection = upgradeCollection;

            _playerStatusManager.OnInitializedAsync
                .Subscribe(_ => ApplyAllUpgrades())
                .AddTo(this);
        }

        public void ApplyAllUpgrades()
        {
            ApplyPlayerUpgrades();
            ApplyWeaponUpgrades();
        }

        void ApplyPlayerUpgrades()
        {
            _playerParamsVariation = new Players.PlayerParams();
            var playerUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeCollection.GetUpgrade(upgradeId) is PlayerUpgrade).ConvertAll(upgradeId => _upgradeCollection.GetUpgrade(upgradeId) as PlayerUpgrade);
            foreach (var playerUpgrade in playerUpgrades)
            {
                _playerParamsVariation += playerParamsVariation;
            }
            _playerStatusManager.ApplyPlayerParamsVariation(_playerParamsVariation);
        }

        void ApplyWeaponUpgrades()
        {
            _weaponList = new List<GameObject>();
            var addWeaponUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeCollection.GetUpgrade(upgradeId) is AddWeaponUpgrade).ConvertAll(upgradeId => _upgradeCollection.GetUpgrade(upgradeId) as AddWeaponUpgrade);
            foreach (var addWeaponUpgrade in addWeaponUpgrades)
            {
                var _weaponParamsVariation = new Weapons.WeaponParams();
                var weaponUpgrades = _upgradeIdList.FindAll(upgradeId => _upgradeCollection.GetUpgrade(upgradeId) is WeaponUpgrade && (_upgradeCollection.GetUpgrade(upgradeId) as WeaponUpgrade).weaponId == addWeaponUpgrade.weaponId).ConvertAll(upgradeId => _upgradeCollection.GetUpgrade(upgradeId) as WeaponUpgrade);
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
