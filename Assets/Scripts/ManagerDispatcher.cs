using UnityEngine;
using UniRx;
using Players;
using Upgrades;

public class ManagerDispatcher : MonoBehaviour
{
    // TODO: refactor dispatchers
    [SerializeField] private Battle _battle;
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private UpgradeSelector _upgradeSelector;
    [SerializeField] private UpgradeSelectorPresenter _upgradeSelectorPresenter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerManager.Initialize(_battle);

        var upgradeSelectorView = _upgradeSelector.GetComponent<UpgradeSelectorView>();
        Debug.Log(upgradeSelectorView);
        _upgradeSelectorPresenter.Initialize(_upgradeSelector, upgradeSelectorView);
    }
}
