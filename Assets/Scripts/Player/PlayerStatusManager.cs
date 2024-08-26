using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    private float maxHealth;
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReadPlayerPropertyDataAsset();
        GetComponent<Health>().Initialize(maxHealth);
        GetComponent<PlayerController>().Initialize(speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadPlayerPropertyDataAsset()
    {
        var path = "Data/PlayerPropertyData";
        var playerParameter = Resources.Load<PlayerPropertyAsset>(path);

        this.speed = playerParameter.speed;
        this.maxHealth = playerParameter.maxHealth;
    }
}
