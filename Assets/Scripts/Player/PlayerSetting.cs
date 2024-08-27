using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerSetting", menuName = "Scriptable Objects/PlayerSetting")]
    public class PlayerSetting : ScriptableObject
    {
        public float maxHealth;
        public float speed;
    }
}
