using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Upgrades
{
    public enum UpgradeType
    {
        Player,
        AddWeapon,
        Weapon
    }

    [Serializable]
    public struct UpgradeData
    {
        public string id;
        public string upgradeName;
        public UpgradeType upgradeType;
        public Players.PlayerParams playerParamsVariation;
        public Weapons.WeaponParams weaponParamsVariation;
        public GameObject weaponPrefab;
    }

    [CreateAssetMenu(fileName = "UpgradeSetting", menuName = "Scriptable Objects/UpgradeSetting")]
    public class UpgradeSetting : ScriptableObject
    {
        public List<UpgradeData> upgradeDataList;

        public UpgradeData GetUpgradeData(UpgradeId id)
        {
            return upgradeDataList.Find(data => data.id == id.ToString());
        }

        private List<string> CreateUpgradeIdList()
        {
            List<string> upgradeIdList = new List<string>();

            foreach (var upgradeData in upgradeDataList)
            {
                if (upgradeIdList.Contains(upgradeData.id))
                {
                    throw new Exception("Upgrade ID is duplicated: " + upgradeData.id);
                }
                upgradeIdList.Add(upgradeData.id);
            }

            return upgradeIdList;
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [ContextMenu("Create Enum File")]
        public void CreateEnumFile()
        {
            string enumName = "UpgradeId";
            string enumFilePath = "Assets/Scripts/Upgrade/UpgradeId.cs";

            if (string.IsNullOrEmpty(enumName) || string.IsNullOrEmpty(enumFilePath))
            {
                Debug.LogError("EnumName or EnumFilePath is empty.");
                return;
            }

            List<string> upgradeIdList;

            try
            {
                upgradeIdList = CreateUpgradeIdList();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }

            if (upgradeIdList == null || upgradeIdList.Count == 0)
            {
                Debug.LogError("UpgradeIdList is empty.");
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("namespace Upgrades");
            builder.AppendLine("{");
            builder.AppendLine("    public enum " + enumName);
            builder.AppendLine("    {");

            foreach (var upgradeId in upgradeIdList)
            {
                builder.AppendLine("        " + upgradeId + ",");
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");

            File.WriteAllText(enumFilePath, builder.ToString());
        }
    }
}
