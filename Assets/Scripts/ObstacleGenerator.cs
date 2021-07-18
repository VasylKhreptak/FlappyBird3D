using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private float _minScatter = 2f;
    [SerializeField] private float _maxScatter = 2f;
    [SerializeField] private Transform _spawnPlace;
    public Coroutine coroutine = null;

    public void StartGeneratingObstacles()
    {
        if (coroutine == null)
            coroutine = StartCoroutine(GenerateObstacle());
    }

    public void StopGeneratingObstacles()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator GenerateObstacle()
    {
        while (true)
        {
            Instantiate(_obstacle,
         _spawnPlace.position + new Vector3(0, Random.Range(_minScatter, _maxScatter), 0),
         _spawnPlace.rotation);

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
