using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _startTips;
    private bool _isTipsEnabled = true;
    [SerializeField] private TextMeshProUGUI _scoreUI;
    public int _score = 0;
    [SerializeField] private GameObject[] _gameOverInfo;
    [SerializeField] private MedalManager _medalManager;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _record;
    [SerializeField] private AnimationManager _animationManager;

    public void ClearTipsOnScreen()
    {
        if (_isTipsEnabled)
        {
            _startTips.SetActive(false);

            _isTipsEnabled = false;
        }
    }

    public void SetActiveScore(bool value)
    {
        if (_scoreUI.enabled != value)
            _scoreUI.enabled = value;
    }

    public void IncreaseScoreByOne()
    {
        _score++;
        UpdateRecord();
        UpdateText();
    }

    private void UpdateRecord()
    {
        if (_score > PlayerPrefs.GetInt("Record"))
            PlayerPrefs.SetInt("Record", _score);
    }
    private void UpdateText()
    {
        _scoreUI.text = _score.ToString();
    }
    public void ShowGameOverInfo()
    {
        StartCoroutine(ShowGameOverInfoCoroutine());
    }

    private IEnumerator ShowGameOverInfoCoroutine()
    {
        foreach (var info in _gameOverInfo)
            info.SetActive(true);

        _record.text = PlayerPrefs.GetInt("Record").ToString();
        _currentScore.text = _score.ToString();
        _record.enabled = true;
        _currentScore.enabled = true;

        _medalManager.SetMedal();

        _animationManager.StartMedalAnimation();

        yield return null;
    }
}
