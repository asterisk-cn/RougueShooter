using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weaponList;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerCore _owner;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _weaponList = new List<GameObject>();

            foreach (Transform child in transform)
            {
                var weapon = child.gameObject;
                if (weapon.TryGetComponent(out BasicGun _))
                {
                    weapon.GetComponent<BasicGun>().Initialize(_owner, _input);
                    _weaponList.Add(weapon);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateWeapons(List<GameObject> weaponList)
        {
            ClearWeapons();
            foreach (var weapon in weaponList)
            {
                var weaponInstance = Instantiate(weapon, transform);
                weaponInstance.GetComponent<BasicGun>().Initialize(_owner, _input);
                _weaponList.Add(weaponInstance);
            }
        }

        void ClearWeapons()
        {
            foreach (var weapon in _weaponList)
            {
                Destroy(weapon);
            }
            _weaponList.Clear();
        }
    }
}
