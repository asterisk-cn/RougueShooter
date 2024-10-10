using UnityEngine;
using System.Collections.Generic;
using R3;
using ObservableCollections;
using VContainer;
using VContainer.Unity;
using Upgrades;
using Managers;

namespace Players
{
    public class PlayerProvider : MonoBehaviour
    {
        public ObservableList<PlayerCore> Players => _players;
        public Observable<int> OnPlayerDeadAsObservable => _onPlayerDead;

        private ObservableList<PlayerCore> _players = new ObservableList<PlayerCore>();
        private Subject<int> _onPlayerDead = new Subject<int>();

        [SerializeField] private List<PlayerCore> _defaultPlayers;
        private Battle _battle;
        private UpgradeCollection _upgradeCollection;

        [Inject]
        public void Construct(Battle battle, UpgradeCollection upgradeCollection)
        {
            _battle = battle;
            _upgradeCollection = upgradeCollection;

            PostInitialize();
        }

        private void PostInitialize()
        {
            SetupDefaultPlayers();

            _battle.CurrentState
                .Where(s => s == BattleState.Battle)
                    .Subscribe(_ => ResetPlayers())
                    .AddTo(this);
        }

        void SetupDefaultPlayers()
        {
            foreach (var p in _defaultPlayers)
            {
                SetupPlayer(p);
            }
        }

        private void SetupPlayer(PlayerCore player)
        {
            player.Initialize(_players.Count);
            _players.Add(player);
            Debug.Log($"Player {player.PlayerId} is added.");

            var upgradeManager = player.gameObject.GetComponent<UpgradeManager>();
            upgradeManager.Initialize(_upgradeCollection);

            player.PlayerDeadAsync
                .Subscribe(_ => _battle.OnPlayerDead(player.PlayerId));
        }

        public void ResetPlayers()
        {
            foreach (var p in _players)
            {
                p.Reset();
            }
        }
    }
}
