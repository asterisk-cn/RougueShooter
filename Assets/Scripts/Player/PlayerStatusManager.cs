using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class PlayerStatusManager : MonoBehaviour
    {
        [SerializeField] PlayerCore _player;

        PlayerParams _defaultPlayerParams;
        PlayerParams _currentPlayerParams;

        [SerializeField] Upgrades.UpgradeManager _upgradeManager;

        void Awake()
        {
            LoadPlayerSetting();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _player.SetPlayerParams(_currentPlayerParams);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ApplyPlayerParamsVariation(PlayerParams playerParamsVariation)
        {
            _currentPlayerParams = new PlayerParams();
            _currentPlayerParams = _defaultPlayerParams + playerParamsVariation;

            _player.SetPlayerParams(_currentPlayerParams);
        }

        void LoadPlayerSetting()
        {
            var path = "Data/PlayerSetting";
            var playerParams = Resources.Load<PlayerSetting>(path);

            this._defaultPlayerParams = playerParams.playerParams;
            this._currentPlayerParams = _defaultPlayerParams;
        }
    }
}
