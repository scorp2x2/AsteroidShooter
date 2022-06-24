using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    SignalBus _signalBus;
    GameStats _gameStats;
    Spawner _spawner;

    public GameObject game;
    public Text textRecords;
    public GameObjectContext player;
    public InputField inputFieldName;

    void Start()
    {
        game.SetActive(false);
        gameObject.SetActive(true);
        inputFieldName.text = _gameStats.NamePlayer;
    }

    [Inject]
    public void Construct(SignalBus signalBus, GameStats gameStats, Spawner spawner)
    {
        _signalBus = signalBus;
        _gameStats = gameStats;
        _spawner = spawner;
    }

    public void InputFieldEnter(string newName)
    {
        Debug.Log(inputFieldName.text);
        _gameStats.NamePlayer = inputFieldName.text;
    }

    public void ButtonStartGame()
    {
        game.SetActive(true);
        gameObject.SetActive(false);

        _spawner.IsSpawn = true;
        _signalBus.Fire<StartGameSignal>();
    }

    public void ButtonRecords()
    {
        textRecords.text = "";

        int index = 1;
        foreach (var item in _gameStats.Scores)
        {
            textRecords.text += $"{index}) {item.Key}: {item.Value}\n";
            index++;
        }
    }

    public void ButtonSettings()
    {

    }

    public void ButtonExit()
    {
        Application.Quit();
    }
}
