using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    [SerializeField] private Mesh _backgroundDay;
    [SerializeField] private Mesh _backgroundNight;
    [SerializeField] private Light _dirLight;
    [SerializeField] private MeshFilter _backGround;

    void Start()
    {
        bool result = true;

        result = Random.Range(0, 2) == 1 ? true : false;

        ChangeBG(result);
    }

    void ChangeBG(bool result)
    {
        _backGround.mesh = result ? _backgroundDay : _backgroundNight;

        _dirLight.intensity = result ? 0.84f : 0.54f;
    }
}
