using UniRx;
using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class PlayerManager : MonoBehaviour
    {
        public IReactiveCollection<GameObject> Players => _players;

        private ReactiveCollection<GameObject> _players = new ReactiveCollection<GameObject>();

        [SerializeField] private List<GameObject> _defaultPlayers = new List<GameObject>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (var p in _defaultPlayers)
            {
                _players.Add(p);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
