using UnityEngine;
using System;

namespace Weapon.Bullets
{
    [Serializable]
    public class BulletParams
    {
        public GameObject owner;
        public float speed;
        public float damage;
    }
}
