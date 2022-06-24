using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : IFixedTickable, IInitializable
{
    Asteroid.Factory _factory;
    SignalBus _signalBus;
    Settings _settings;

    int countEnemy;

    [System.Serializable]
    public class Settings
    {
        public Vector2 Speed;
        public Vector2 HP;
        public Vector2 Damage;

        public int MaxCountAsteroids;
        public float NumEnemiesStartAmount;

        public float DelaySpawns;
    }

    public Spawner(Asteroid.Factory factory, SignalBus signalBus, Settings settings)
    {
        _factory = factory;
        _signalBus = signalBus;
        _settings = settings;
    }

    float timeLastSpawn;

    public void FixedTick()
    {
        if (countEnemy < _settings.MaxCountAsteroids && timeLastSpawn + _settings.DelaySpawns < Time.time)
        {
            Spawn();
            timeLastSpawn = Time.time;
        }
    }

    void Spawn()
    {
        var speed = Random.Range(_settings.Speed.x, _settings.Speed.y);
        var hp = Random.Range(_settings.HP.x, _settings.HP.y);
        var damage = Random.Range(_settings.Damage.x, _settings.Damage.y);

        var asteroid = _factory.Create(speed, hp, damage);
        asteroid.transform.localPosition = GetRandomPosition();

        countEnemy++;
    }

    Vector2 GetRandomPosition()
    {
        var y = Random.Range(-Screen.height / 2, Screen.height / 2);
        return new Vector2(Screen.width / 2, y);
    }

    public void Initialize()
    {
        _signalBus.Subscribe<AsteroidDeadSignal>(OnAsteroidDead);
    }

    private void OnAsteroidDead()
    {
        countEnemy--;
    }
}
