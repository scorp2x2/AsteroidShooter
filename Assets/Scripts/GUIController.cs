using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUIController : MonoBehaviour
{
    Player _player;
    Player.Settings _playerSettings;

    public float scoreCount;
    public Image imageHp;
    public Text textCountKillAsteroids;

    [Inject]
    public void Construct(Player player, Player.Settings playerSettings, SignalBus signalBus)
    {
        _player = player;
        _playerSettings = playerSettings;

        signalBus.Subscribe<AsteroidDeadSignal>(OnAsteroidDead);
    }

    private void OnAsteroidDead(AsteroidDeadSignal signal)
    {
        scoreCount += signal.ScoreCount;
    }

    // Update is called once per frame
    void Update()
    {
        imageHp.fillAmount = _player.Hp / _playerSettings.MaxHp;
        textCountKillAsteroids.text = Mathf.RoundToInt(scoreCount).ToString();
    }
}
