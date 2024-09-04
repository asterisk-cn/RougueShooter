using System;

namespace Weapons
{
    [Serializable]
    public class WeaponParams
    {
        public float fireRate = 0.5f;
        public Bullets.BulletParams bulletParams = new Bullets.BulletParams();

        public static WeaponParams operator +(WeaponParams a, WeaponParams b)
        {
            return new WeaponParams
            {
                fireRate = a.fireRate + b.fireRate,
                bulletParams = a.bulletParams + b.bulletParams
            };
        }
    }
}
