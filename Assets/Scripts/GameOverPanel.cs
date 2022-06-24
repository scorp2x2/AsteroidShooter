using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverPanel : MonoBehaviour
{
    GameStats _gameStats;

    public Text textScoreCount;
    public Text textBestScoreCount;

    void Start()
    {
        gameObject.SetActive(false);
    }

    [Inject]
    public void Construct(SignalBus signalBus, GameStats gameStats)
    {
        _gameStats = gameStats;

        signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDead);
    }

    private void OnPlayerDead()
    {
        gameObject.SetActive(true);
        _gameStats.SaveScore();

        DrawInfo();
    }

    void DrawInfo()
    {
        textScoreCount.text = Mathf.RoundToInt(_gameStats.ScoreCount).ToString();

        var maxScore = _gameStats.Scores.First();
        textBestScoreCount.text = $"{maxScore.Key}: {maxScore.Value}";
    }

}
