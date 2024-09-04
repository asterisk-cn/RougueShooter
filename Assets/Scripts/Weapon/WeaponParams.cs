using System;

namespace Weapons
{
    [Serializable]
    public class WeaponParams
    {
        public float fireRate;
        public float damage;

        public static WeaponParams operator +(WeaponParams a, WeaponParams b)
        {
            return new WeaponParams
            {
                fireRate = a.fireRate + b.fireRate,
                damage = a.damage + b.damage
            };
        }
    }
}
