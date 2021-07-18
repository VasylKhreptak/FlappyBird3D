using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _timeOfFall;
    [SerializeField] private ObstacleGenerator _obstacleGenerator;
    [SerializeField] private HingeJoint _birdHingeJoint;
    [SerializeField] private AnimationManager _animationManager;

    private Coroutine _coroutine = null;

    public void StartGame()
    {
        SceneManager.LoadScene(1); // GamePlay scene
    }

    public void GameOver()
    {
        if (_coroutine != null) return;
        _coroutine = StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        DisableAllBirdMovement();
        DisableObstacleMovement();

        _animationManager.ScoreFadeAnimation();

        yield return new WaitForSeconds(0.40f);

        _uiManager.SetActiveScore(false);

        yield return new WaitForSeconds(_timeOfFall);

        _uiManager.ShowGameOverInfo();
        _animationManager.StartCoroutine(_animationManager.StartGameOverAnimationCoroutine());
    }
    public void DisableAllBirdMovement()
    {
        _playerMovement.enabled = false;
        _playerAnimator.enabled = false;

        Destroy(_birdHingeJoint);
    }

    private void DisableObstacleMovement()
    {
        foreach (var ObstacleMovement in FindObjectsOfType<ObstacleMovement>())
            ObstacleMovement.enabled = false;

        foreach (var ObstaclePossitionLimeter in FindObjectsOfType<ObstaclePossitionLimeter>())
            ObstaclePossitionLimeter.enabled = false;

        _obstacleGenerator.StopGeneratingObstacles();
    }
}
