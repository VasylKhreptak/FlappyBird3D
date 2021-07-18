using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 10f;
    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _torquePower = 30f;
    [SerializeField] private float _continuousTorquePower = 10f;
    [SerializeField] private ObstacleGenerator _obstacleGenerator;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AnimationManager _animationManager;
    [SerializeField] private WorldTeleportController _worldTeleportController;
    [SerializeField] private PlayerMovement _playerMovement;

    void Start()
    {
        _playerRB.isKinematic = true;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                SetMovement();
        }
        else if (Input.GetMouseButtonDown(0))
            SetMovement();

        _playerRB.AddTorque(0, 0, _continuousTorquePower);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            _uiManager.IncreaseScoreByOne();
            _audioManager.PlayScoreClip();
        }
        else if (other.tag == "Lava")
        {
            _animationManager.StartBurning();
            _gameManager.GameOver();
        }
        else if (other.tag == "DownCollider")
            _gameManager.GameOver();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
            StartEndGameActions();
        else if (_worldTeleportController.isTeleportAllowed(collision))
        {
            _playerMovement.enabled = false;

            _worldTeleportController.ChooseWorldToTeleport(collision);

            _playerCollider.enabled = false;
        }
        else if (_worldTeleportController._currentScene == "ParadiseWorld")
            _gameManager.GameOver();
        else
            StartEndGameActions();
    }
    private void StartEndGameActions()
    {
        _gameManager.GameOver();
        _audioManager.PlayPunchClip();
    }

    private void SetMovement()
    {
        if (_playerRB.isKinematic == true)
            _playerRB.isKinematic = false;

        _uiManager.SetActiveScore(true);

        _playerRB.velocity = Vector3.up * _jumpVelocity;
        _playerRB.AddTorque(0, 0, -_torquePower);

        _obstacleGenerator.StartGeneratingObstacles();

        _audioManager.PlayJumpClip();

        _animationManager.StartCoroutine(_animationManager.StartFirstMovementAnimationCoroutine());
    }
}
