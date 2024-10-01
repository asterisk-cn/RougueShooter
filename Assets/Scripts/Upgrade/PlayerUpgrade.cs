using UnityEngine;

namespace Upgrades
{
    public class PlayerUpgrade : IUpgrade
    {
        string _upgradeName;
        Players.PlayerParams _playerParamsVariation;

        public string upgradeName
        {
            get { return _upgradeName; }
        }

        public Players.PlayerParams playerParamsVariation
        {
            get { return _playerParamsVariation; }
        }

        public void Initialize(UpgradeData upgradeData)
        {
            _upgradeName = upgradeData.upgradeName;
            _playerParamsVariation = upgradeData.playerParamsVariation;
        }
    }
}
