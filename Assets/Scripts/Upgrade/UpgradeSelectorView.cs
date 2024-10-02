using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using R3;
using System;
using TMPro;

namespace Upgrades
{
    public class UpgradeSelectorView : MonoBehaviour
    {
        [SerializeField] private List<Button> _upgradeSelectButtons;

        public Observable<int> OnUpgradeSelectedAsObservable => _onUpgradeSelected;

        private Subject<int> _onUpgradeSelected { get; } = new Subject<int>();
        private int _upgradePlayerId = -1;
        private int _selectedUpgradeIndex;

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

            _onUpgradeSelected.AddTo(this);
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

        public void SetPlayerId(int playerId)
        {
            _upgradePlayerId = playerId;
            Debug.Log($"Player {playerId} is upgradable.");
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
