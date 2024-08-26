using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPropertyData", menuName = "Scriptable Objects/PlayerPropertyAsset")]
public class PlayerPropertyAsset : ScriptableObject
{
    public float maxHealth;
    public float speed;
}
