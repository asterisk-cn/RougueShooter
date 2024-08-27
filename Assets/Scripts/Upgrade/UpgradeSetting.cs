using UnityEngine;
using System;
using System.Collections.Generic;

namespace Upgrade
{
    public enum UpgradeId
    {
        HEALTH01,
        SPEED01,
    }

    public enum UpgradeType
    {
        Player,
        AddWeapon,
        Weapon
    }

    [Serializable]
    public struct UpgradeData
    {
        public UpgradeId id;
        public string upgradeName;
        public UpgradeType upgradeType;
        public Players.PlayerParams extPlayerParams;
        public Weapons.WeaponParams extWeaponParams;
        public GameObject weaponPrefab;
    }

    [CreateAssetMenu(fileName = "UpgradeSetting", menuName = "Scriptable Objects/UpgradeSetting")]
    public class UpgradeSetting : ScriptableObject
    {
        public List<UpgradeData> upgradeDataList;
    }
}
