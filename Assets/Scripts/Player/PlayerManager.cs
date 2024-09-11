using UniRx;
using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class PlayerManager : MonoBehaviour
    {
        public IReactiveCollection<PlayerCore> Players => _players;

        private ReactiveCollection<PlayerCore> _players = new ReactiveCollection<PlayerCore>();

        [SerializeField] private List<PlayerCore> _defaultPlayers = new List<PlayerCore>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (var p in _defaultPlayers)
            {
                SetupPlayer(p);
                _players.Add(p);
            }
        }

        void OnPlayerDead(int playerId)
        {
            Debug.Log($"Player {playerId} is dead.");
        }

        private void SetupPlayer(PlayerCore player)
        {
            player.Initialize(_players.Count);
            Debug.Log($"Player {player.PlayerId} is added.");

            player.PlayerDeadAsync
                .Subscribe(_ => OnPlayerDead(player.PlayerId))
                .AddTo(this);
        }
    }
}
