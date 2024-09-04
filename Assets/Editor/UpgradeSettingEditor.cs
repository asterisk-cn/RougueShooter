using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Upgrades
{
    [CustomEditor(typeof(UpgradeSetting))]
    public class UpgradeSettingEditor : Editor
    {
        private ReorderableList reorderableList;

        private void OnEnable()
        {
            SerializedProperty upgradeDataList = serializedObject.FindProperty("upgradeDataList");

            reorderableList = new ReorderableList(serializedObject, upgradeDataList, true, true, true, true);

            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Upgrade Data List");
            };

            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = upgradeDataList.GetArrayElementAtIndex(index);
                rect.y += 2;

                EditorGUI.PropertyField(rect, element, GUIContent.none, true);
            };

            reorderableList.elementHeightCallback = (int index) =>
            {
                SerializedProperty element = upgradeDataList.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + 4;
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            UpgradeSetting upgradeSetting = target as UpgradeSetting;

            if (GUILayout.Button("Create Enum File"))
            {
                upgradeSetting.CreateEnumFile();
            }

            reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
