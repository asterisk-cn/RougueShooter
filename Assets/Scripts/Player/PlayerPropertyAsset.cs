using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerPropertyData", menuName = "Scriptable Objects/PlayerPropertyAsset")]
    public class PlayerPropertyAsset : ScriptableObject
    {
        public float maxHealth;
        public float speed;
    }
}
