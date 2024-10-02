using UnityEngine;
using System.Collections.Generic;
using R3;
using ObservableCollections;
using static Battle;

namespace Players
{
    public class PlayerManager : MonoBehaviour
    {
        public ObservableList<PlayerCore> Players => _players;

        private ObservableList<PlayerCore> _players = new ObservableList<PlayerCore>();

        [SerializeField] private List<PlayerCore> _defaultPlayers = new List<PlayerCore>();
        private Battle _battle;

        public void Initialize(Battle battle)
        {
            _battle = battle;

            SetupDefaultPlayers();

            _battle.State
                .Where(s => s == BattleState.Battle)
                .Subscribe(_ => OnReset())
                .AddTo(this);
        }

        void SetupDefaultPlayers()
        {
            foreach (var p in _defaultPlayers)
            {
                SetupPlayer(p);
            }
        }

        // TODO: SOLID principle
        void OnPlayerDead(int playerId)
        {
            _battle.SetUpgradablePlayer(playerId);
            _battle.SetState(BattleState.Upgrade);
        }

        private void SetupPlayer(PlayerCore player)
        {
            player.Initialize(_players.Count);
            _players.Add(player);
            Debug.Log($"Player {player.PlayerId} is added.");

            player.PlayerDeadAsync
                .Subscribe(_ => OnPlayerDead(player.PlayerId))
                .AddTo(this);
        }

        void OnReset()
        {
            foreach (var p in _players)
            {
                p.Reset();
            }
        }
    }
}
