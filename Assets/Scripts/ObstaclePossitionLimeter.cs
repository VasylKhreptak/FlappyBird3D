using UnityEngine;

public class ObstaclePossitionLimeter : MonoBehaviour
{
    [SerializeField] float _maxXPossition = 26f;
    [SerializeField] Transform _obstacle;

    void Update()
    {
        if (_obstacle.position.x > _maxXPossition)
            Destroy(gameObject);
    }
}
