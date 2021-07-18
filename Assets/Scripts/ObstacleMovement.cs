using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;

    void FixedUpdate()
    {
        transform.position += new Vector3(_movementSpeed, 0, 0);
    }
}
