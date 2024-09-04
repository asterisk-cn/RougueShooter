using System;
using UnityEngine;

namespace Players
{
    [Serializable]
    public class PlayerParams
    {
        public float maxHealth;
        public float speed;

        public static PlayerParams operator +(PlayerParams a, PlayerParams b)
        {
            return new PlayerParams
            {
                maxHealth = a.maxHealth + b.maxHealth,
                speed = a.speed + b.speed
            };
        }
    }
}
