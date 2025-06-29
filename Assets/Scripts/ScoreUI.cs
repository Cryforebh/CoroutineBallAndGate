using Assets.Scripts;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreUI : MonoBehaviour
{
    [SerializeField, Tooltip("Сюда привяжите Ворота")]
    private Gates _hitTrigger;
    [SerializeField, Tooltip("Скорость обновления счета"), Min(0.1f)]
    private float _updateSpeed = 0.6f;

    private TMP_Text _scoreText;
    private int _displayedScore;
    private float _animationProgress;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();

        if (_hitTrigger == null)
        {
            Debug.Log("Вы забыли привязать Ворота!");
            return;
        }

        _displayedScore = GetCalculationScore();
        UpdateText();
    }

    private void Update()
    {
        if (_displayedScore != GetCalculationScore())
        {
            _animationProgress += Time.deltaTime / _updateSpeed;
            _displayedScore = (int)Mathf.Lerp(_displayedScore, GetCalculationScore(), _animationProgress);
            UpdateText();

            if (Mathf.Approximately(_displayedScore, GetCalculationScore()))
            {
                _animationProgress = 0f;
                Debug.Log($"Счет: {_displayedScore}");
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

    private int GetCalculationScore()
    {
        
        return 10 * _hitTrigger.NumberOfHits;
    }
}
