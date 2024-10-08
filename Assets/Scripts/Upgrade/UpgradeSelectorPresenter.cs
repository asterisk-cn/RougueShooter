using R3;
using VContainer.Unity;

namespace Upgrades
{
    public class UpgradeSelectorPresenter : IPostInitializable
    {
        private readonly UpgradeSelector _model;
        private readonly UpgradeSelectorView _view;

        public UpgradeSelectorPresenter(UpgradeSelector model, UpgradeSelectorView view)
        {
            _model = model;
            _view = view;
        }

        public void PostInitialize()
        {
            _model.OnUpgradesUpdatedAsObservable
                .Subscribe(x => _view.SetUpgrades(x));

            _model.IsActive
                .Subscribe(x => _view.SetActive(x));

            _view.OnUpgradeSelectedAsObservable
                .Subscribe(x => _model.ApplyUpgrade(x));
        }
    }
}
