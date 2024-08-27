using UnityEngine;
using UnityEditor;

namespace Upgrade
{
    [CustomPropertyDrawer(typeof(UpgradeData))]
    public class UpgradeDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float singleLineHeight = EditorGUIUtility.singleLineHeight;
            position.height = singleLineHeight;

            // 各プロパティの取得
            SerializedProperty upgradeNameProp = property.FindPropertyRelative("upgradeName");
            SerializedProperty upgradeTypeProp = property.FindPropertyRelative("upgradeType");

            // 各フィールドの表示
            EditorGUI.PropertyField(position, upgradeNameProp, new GUIContent("Upgrade Name"));
            position.y += singleLineHeight + 2;
            EditorGUI.PropertyField(position, upgradeTypeProp, new GUIContent("Upgrade Type"));
            position.y += singleLineHeight + 2;

            var upgradeType = (UpgradeType)upgradeTypeProp.enumValueIndex;

            if (upgradeType == UpgradeType.Player)
            {
                SerializedProperty extPlayerParamsProp = property.FindPropertyRelative("extPlayerParams");
                EditorGUI.PropertyField(position, extPlayerParamsProp, true);
            }
            else if (upgradeType == UpgradeType.AddWeapon)
            {
                SerializedProperty weaponPrefabProp = property.FindPropertyRelative("weaponPrefab");
                EditorGUI.PropertyField(position, weaponPrefabProp, new GUIContent("Weapon Prefab"));
            }
            else if (upgradeType == UpgradeType.Weapon)
            {
                SerializedProperty extWeaponParamsProp = property.FindPropertyRelative("extWeaponParams");
                EditorGUI.PropertyField(position, extWeaponParamsProp, true);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight * 2 + 4; // 基本の高さ

            var upgradeType = (UpgradeType)property.FindPropertyRelative("upgradeType").enumValueIndex;

            if (upgradeType == UpgradeType.Player)
            {
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("extPlayerParams"), true) + 2;
            }
            else if (upgradeType == UpgradeType.AddWeapon)
            {
                height += EditorGUIUtility.singleLineHeight + 2;
            }
            else if (upgradeType == UpgradeType.Weapon)
            {
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("extWeaponParams"), true) + 2;
            }

            return height;
        }
    }
}
