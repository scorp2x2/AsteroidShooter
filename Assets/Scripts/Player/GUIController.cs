using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUIController : MonoBehaviour
{
    Player _player;
    GameStats _gameStats;

    public Image imageHp;
    public Text textCountKillAsteroids;

    [Inject]
    public void Construct(Player player, GameStats gameStats)
    {
        _player = player;
        _gameStats = gameStats;
    }

    // Update is called once per frame
    void Update()
    {
        imageHp.fillAmount = _player.Hp / _player.MaxHp;
        textCountKillAsteroids.text = Mathf.RoundToInt(_gameStats.ScoreCount).ToString();
    }
}
