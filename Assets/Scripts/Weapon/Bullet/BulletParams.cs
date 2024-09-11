using UnityEngine;
using System;

namespace Weapons.Bullets
{
    [Serializable]
    public class BulletParams
    {
        public float speed = 15f;
        public float damage = 10f;

        public static BulletParams operator +(BulletParams a, BulletParams b)
        {
            return new BulletParams
            {
                speed = a.speed + b.speed,
                damage = a.damage + b.damage
            };
        }
    }
}
