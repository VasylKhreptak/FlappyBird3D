using UnityEngine;

public class RandomBird : MonoBehaviour
{
    [SerializeField] private Mesh _redBird;
    [SerializeField] private Mesh _blueBird;
    [SerializeField] private Mesh _yellowBird;
    [SerializeField] private MeshFilter _bird;

    void Start()
    {
        ChangeBird((int)Random.Range(1, 4));
    }
    void ChangeBird(float result)
    {
        if (result == 1)
            _bird.mesh = _yellowBird;
        else if (result == 2)
            _bird.mesh = _blueBird;
        else
            _bird.mesh = _redBird;
    }
}
