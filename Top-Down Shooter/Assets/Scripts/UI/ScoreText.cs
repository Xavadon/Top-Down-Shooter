using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private float _score;

    private void OnEnable()
    {
        UnitHealth.EnemyDied += IncrementScore;
    }

    private void OnDisable()
    {
        UnitHealth.EnemyDied -= IncrementScore;
    }

    private void IncrementScore()
    {
        _score += 65;
        _scoreText.text = $"{_score}";
    }
}
