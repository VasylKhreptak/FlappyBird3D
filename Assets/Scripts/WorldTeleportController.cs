using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WorldTeleportController : MonoBehaviour
{
    public string _currentScene;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Collider _downCollider;
    [SerializeField] float _timeBeforeTeleportation = 1f;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _downTeleportationImpulse = 60f;
    [SerializeField] private float _upTeleportationImpulse = 40f;

    void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
    }

    public bool isTeleportAllowed(Collision collision)
    {
        if (_currentScene == "GamePlay")
            if (collision.collider.tag == "DownCollider")
                if (collision.impulse.magnitude > _downTeleportationImpulse)
                    return true;
        if (_currentScene == "GamePlay")
            if (collision.collider.tag == "UpCollider")
                if (collision.impulse.magnitude > _upTeleportationImpulse)
                    return true;
        if (_currentScene == "HellWorld")
            if (collision.collider.tag == "UpCollider")
                if (collision.impulse.magnitude > _upTeleportationImpulse)
                    return true;
        if (_currentScene == "ParadiseWorld")
            if (collision.collider.tag == "DownCollider")
                if (collision.impulse.magnitude > _downTeleportationImpulse + 20f)
                    return true;

        return false;
    }

    public void ChooseWorldToTeleport(Collision collision)
    {
        if (_currentScene == "GamePlay")
            if (collision.collider.tag == "DownCollider")
                StartCoroutine(TeleportToHellCoroutine());

        if (_currentScene == "GamePlay")
            if (collision.collider.tag == "UpCollider")
                StartCoroutine(TeleportToParadiseCoroutine());

        if (_currentScene == "HellWorld")
            if (collision.collider.tag == "UpCollider")
                StartCoroutine(TeleportToEarthCoroutine());

        if (_currentScene == "ParadiseWorld")
            if (collision.collider.tag == "DownCollider")
                StartCoroutine(TeleportToEarthCoroutine());
    }

    private IEnumerator TeleportToHellCoroutine()
    {
        _downCollider.isTrigger = true;

        yield return new WaitForSeconds(_timeBeforeTeleportation);

        SceneManager.LoadScene("HellWorld");
    }
    private IEnumerator TeleportToEarthCoroutine()
    {
        _gameManager.DisableAllBirdMovement();
        _playerRigidbody.isKinematic = true;

        yield return new WaitForSeconds(_timeBeforeTeleportation);

        SceneManager.LoadScene("GamePlay");
    }
    private IEnumerator TeleportToParadiseCoroutine()
    {
        _downCollider.isTrigger = true;
        _playerRigidbody.isKinematic = true;

        yield return new WaitForSeconds(_timeBeforeTeleportation);

        SceneManager.LoadScene("ParadiseWorld");
    }
}
