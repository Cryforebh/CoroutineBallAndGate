using Assets.Scripts;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Gates))]
public class ScoreUI : MonoBehaviour
{
    [SerializeField, Tooltip("Сюда привяжите TMP_Text")]
    private TMP_Text _scoreText;
    [SerializeField, Tooltip("Скорость обновления счета")]
    private float _updateSpeed = 0.5f;

    private Gates _gates;
    private int _displayedScore;
    private float _animationProgress;

    private void Awake()
    {
        if (_scoreText == null)
        {
            Debug.Log("Вы забыли привязать TMP_Text!");
            return;
        }

        _gates = GetComponent<Gates>();
        _displayedScore = _gates.Score;
        UpdateText();
    }

    private void Update()
    {
        if (_displayedScore != _gates.Score)
        {
            _animationProgress += Time.deltaTime / _updateSpeed;
            _displayedScore = (int)Mathf.Lerp(_displayedScore, _gates.Score, _animationProgress);
            UpdateText();

            if (Mathf.Approximately(_displayedScore, _gates.Score))
            {
                _animationProgress = 0f;
            }
        }
    }

    private void UpdateText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Счет: {_displayedScore}";
        }
    }
}
