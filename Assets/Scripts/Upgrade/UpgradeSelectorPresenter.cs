using UnityEngine;
using R3;

namespace Upgrades
{
    public class UpgradeSelectorPresenter : MonoBehaviour
    {
        public void Initialize(UpgradeSelector model, UpgradeSelectorView view)
        {
            view.Initialize();
            model.Initialize();

            model.OnUpgradesUpdatedAsObservable
                .Subscribe(x => view.SetUpgrades(x))
                .AddTo(this);

            model.IsActive
                .Subscribe(x => view.SetActive(x))
                .AddTo(this);

            view.OnUpgradeSelectedAsObservable
                .Subscribe(x => model.ApplyUpgrade(x))
                .AddTo(this);
        }
    }
}
