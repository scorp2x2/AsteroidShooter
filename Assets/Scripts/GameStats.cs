using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using System.IO;

public class GameStats
{
    SignalBus _signalBus;

    public string NamePlayer { get; set; }
    public float ScoreCount { get; private set; }

    public List<KeyValuePair<string, int>> Scores { get; private set; }

    public GameStats(SignalBus signalBus, string namePlayer)
    {
        _signalBus = signalBus;
        NamePlayer = namePlayer;
        ScoreCount = 0;

        _signalBus.Subscribe<AsteroidDeadSignal>(OnAsteroidDead);
        _signalBus.Subscribe<StartGameSignal>(() => { ScoreCount = 0; });

        LoadSaveScore();
    }

    private void OnAsteroidDead(AsteroidDeadSignal signal)
    {
        ScoreCount += signal.ScoreCount;
    }

    public void SaveScore()
    {
        if (ScoreCount == 0) return;
        Scores.Add(new KeyValuePair<string, int>(NamePlayer, Mathf.RoundToInt(ScoreCount)));
        Scores = Scores.OrderBy(a => -a.Value).ToList();

        string text = $"{NamePlayer}:{Mathf.RoundToInt(ScoreCount)}\n";
        string fileName = Application.dataPath + @"\GameSave";
        File.AppendAllText(fileName, text);
    }

    void LoadSaveScore()
    {
        Scores = new List<KeyValuePair<string, int>>();

        string fileName = Application.dataPath + @"\GameSave";
        if (!File.Exists(fileName)) return;
        var texts = File.ReadAllLines(fileName);

        foreach (var item in texts)
        {
            if (item == "") continue;
            var record = item.Split(':');
            Scores.Add(new KeyValuePair<string, int>(record[0], Convert.ToInt32(record[1])));
        }

        Scores = Scores.OrderBy(a => -a.Value).ToList();
    }
}
