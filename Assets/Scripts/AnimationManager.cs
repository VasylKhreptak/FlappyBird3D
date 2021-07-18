using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    //FirstMovementAnimation
    [SerializeField] private Animation[] _startTipsAnimations;
    [SerializeField] private Animation _scoreAppearAnimation;
    private bool _isAnimatedAppear = false;
    //FirstMovementAnimation

    //GameOverAnimation
    [SerializeField] private Animation _gameOverTextAnimation;
    [SerializeField] private Animation _resultTableAnimation;
    [SerializeField] private Animation _medalAnimation;
    [SerializeField] private Animation _playButtonAnimation;
    [SerializeField] private Animation _scoreFadeAnimation;
    [SerializeField] private Animation _recordTextAnimation;
    [SerializeField] private Animation _currentScoreTextAnimation;
    [SerializeField] private GameObject _eventSystem;
    //GameOverAnimation

    //Medal
    [SerializeField] private Rigidbody _medalRigidBody;
    [SerializeField] private float _speedOfMedalAnimation;
    //Medal

    //LavaBurning
    [SerializeField] private ParticleSystem _lavaParticle;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _lavaDensity = 7f;
    [SerializeField] private AudioSource _lavaBurningSound;
    private Coroutine _coroutine = null;
    //LavaBurning

    public void StartMedalAnimation()
    {
        _medalRigidBody.AddTorque(Vector3.up * _speedOfMedalAnimation);
    }

    public IEnumerator StartFirstMovementAnimationCoroutine()
    {
        if (!_isAnimatedAppear)
        {
            foreach (var animation in _startTipsAnimations)
                animation.Play();

            _scoreAppearAnimation.Play();

            _isAnimatedAppear = true;

            yield return new WaitForSeconds(_startTipsAnimations.Length);

            _uiManager.ClearTipsOnScreen();
        }
    }
    public IEnumerator StartGameOverAnimationCoroutine()
    {
        _uiManager.ShowGameOverInfo();

        _gameOverTextAnimation.Play();

        yield return new WaitForSeconds(0.4f);

        _resultTableAnimation.Play();

        yield return new WaitForSeconds(0.35f);

        _currentScoreTextAnimation.Play();
        _recordTextAnimation.Play();

        yield return new WaitForSeconds(0.15f);

        _playButtonAnimation.Play();
        _medalAnimation.Play();
        _eventSystem.SetActive(true);
    }

    public void ScoreFadeAnimation()
    {
        _scoreFadeAnimation.Play("TextFadeAnimation");
    }

    public void StartBurning()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(StartBurningCoroutine());
    }

    private IEnumerator StartBurningCoroutine()
    {
        _lavaParticle.Play();
        _lavaBurningSound.Play();
        _playerRigidbody.drag = _lavaDensity;
        _playerRigidbody.angularDrag = _lavaDensity;

        yield return new WaitForSecondsRealtime(_lavaBurningSound.clip.length);
        _lavaParticle.Stop();

    }
}
