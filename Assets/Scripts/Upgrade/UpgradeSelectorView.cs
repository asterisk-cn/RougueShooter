using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UniRx;
using System;
using TMPro;

namespace Upgrades
{
    public class UpgradeSelectorView : MonoBehaviour
    {
        [SerializeField] List<Button> _upgradeSelectButtons;

        public IObservable<int> OnUpgradeSelectedAsObservable => _onUpgradeSelected;

        Subject<int> _onUpgradeSelected { get; } = new Subject<int>();
        int upgradePlayerId = -1;
        int _selectedUpgradeIndex;

        public int selectedUpgradeIndex
        {
            get { return _selectedUpgradeIndex; }
        }

        public void Awake()
        {
            foreach (var button in _upgradeSelectButtons)
            {
                button.onClick.AddListener(() => OnClickUpgradeButton(_upgradeSelectButtons.IndexOf(button)));
            }
        }

        public void Initialize()
        {
            ResetSelection();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void OnClickUpgradeButton(int index)
        {
            SelectUpgrade(index);
        }

        public void SetUpgrades(List<IUpgrade> upgrades)
        {
            if (upgrades.Count > _upgradeSelectButtons.Count)
            {
                Debug.LogError("Not enough buttons for all upgrades");
                return;
            }
            for (int i = 0; i < upgrades.Count; i++)
            {
                _upgradeSelectButtons[i].GetComponentInChildren<TMP_Text>().text = upgrades[i].upgradeName;
            }
        }

        void SelectUpgrade(int index)
        {
            _selectedUpgradeIndex = index;
            _onUpgradeSelected.OnNext(index);
        }

        public void ResetSelection()
        {
            _selectedUpgradeIndex = -1;
        }
    }
}
