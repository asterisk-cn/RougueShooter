using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = _targetTransform.position;
    }
}
