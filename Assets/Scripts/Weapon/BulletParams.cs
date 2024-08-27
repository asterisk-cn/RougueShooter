using UnityEngine;
using System;

namespace Weapon.Bullets
{
    [Serializable]
    public struct BulletParams
    {
        public GameObject owner;
        public float speed;
        public float damage;
    }
}
