using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class Player
{
    Rigidbody2D _rigidBody;
    SignalBus _signalBus;
    public float MaxHp { get; private set; }
    public float Hp { get; private set; }

    public bool IsDead { get; private set; }

    [Serializable]
    public class Settings
    {
        public float MaxHp;
    }

    public Vector2 Position
    {
        get => _rigidBody.transform.localPosition;
        private set => _rigidBody.transform.localPosition = value;
    }

    public Player(Rigidbody2D rigidBody, Settings settings, SignalBus signalBus)
    {
        _rigidBody = rigidBody;
        _signalBus = signalBus;
        Hp = settings.MaxHp;
        MaxHp = settings.MaxHp;

        signalBus.Subscribe<StartGameSignal>(OnStartGame);
    }

    private void OnStartGame()
    {
        SetPostion(new Vector2());
        Hp = MaxHp;
        IsDead = false;
    }

    public void AddForce(Vector2 vector)
    {
        _rigidBody.AddForce(vector);
    }

    public void SetPostion(Vector2 vector)
    {
        Position = vector;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        Hp -= damage;
        if (Hp < 0)
        {
            Die();
        }
    }

    void Die()
    {
        IsDead = true;
        _signalBus.Fire<PlayerDiedSignal>();
    }
}
