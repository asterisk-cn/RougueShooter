using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class PlayerStatusManager : MonoBehaviour
    {
        PlayerParams _defaultPlayerParams;
        PlayerParams _currentPlayerParams;

        [SerializeField] Upgrades.UpgradeManager _upgradeManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void CalculatePlayerParams()
        {
            var variation = _upgradeManager.playerParamsVariation;

            _currentPlayerParams.maxHealth = _defaultPlayerParams.maxHealth + variation.maxHealth;
            _currentPlayerParams.speed = _defaultPlayerParams.speed + variation.speed;
        }
    }
}
