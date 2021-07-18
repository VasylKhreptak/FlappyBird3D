using UnityEngine;

public class MedalManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Mesh _bronzeMedal;
    [SerializeField] private Mesh _silverMedal;
    [SerializeField] private Mesh _goldMedal;
    [SerializeField] private Mesh _noMedal;
    [SerializeField] private MeshFilter _medal;

    private float _previousRecord;

    void Start()
    {
        _medal.mesh = _noMedal;
        _previousRecord = PlayerPrefs.GetInt("Record");
    }
    public void SetMedal()
    {
        if (_uiManager._score <= _previousRecord)
            _medal.mesh = _noMedal;
        if (_uiManager._score >= (_previousRecord + 1))
            _medal.mesh = _bronzeMedal;
        if (_uiManager._score >= (_previousRecord + 2))
            _medal.mesh = _silverMedal;
        if (_uiManager._score >= (_previousRecord + 3))
            _medal.mesh = _goldMedal;
    }
}
