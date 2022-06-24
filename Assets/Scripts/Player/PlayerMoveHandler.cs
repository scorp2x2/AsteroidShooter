using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMoveHandler : IFixedTickable
{
    readonly Settings _settings;
    readonly Player _player;
    readonly InputState _inputState;

    [Serializable]
    public class Settings
    {
        public float MoveSpeed;
        public float Offset;
    }

    public PlayerMoveHandler(InputState inputState, Player player, Settings settings)
    {
        _settings = settings;
        _player = player;
        _inputState = inputState;
    }

    public void FixedTick()
    {
        CheckBoundsScreen();

        if (_player.IsDead) return;

        if (_inputState.Left)
        {
            AddForce(Vector2.left * _settings.MoveSpeed);
        }
        if (_inputState.Right)
        {
            AddForce(Vector2.right * _settings.MoveSpeed);
        }
        if (_inputState.Up)
        {
            AddForce(Vector2.up * _settings.MoveSpeed);
        }
        if (_inputState.Down)
        {
            AddForce(Vector2.down * _settings.MoveSpeed);
        }
    }

    void AddForce(Vector2 vector)
    {
        _player.AddForce(vector);
    }

    void CheckBoundsScreen()
    {
        if (_player.Position.x < -Screen.width / 2 + _settings.Offset)
        {
            _player.SetPostion(new Vector2(-Screen.width / 2 + _settings.Offset, _player.Position.y));
            AddForce(Vector2.right);
        }
        else if (_player.Position.x > Screen.width / 2 - _settings.Offset)
        {
            _player.SetPostion(new Vector2(Screen.width / 2 - _settings.Offset, _player.Position.y));
            AddForce(Vector2.left);
        }
        if (_player.Position.y < -Screen.height / 2 + _settings.Offset)
        {
            _player.SetPostion(new Vector2(_player.Position.x, -Screen.height / 2 + _settings.Offset));
            AddForce(Vector2.up);
        }
        else if (_player.Position.y > Screen.height / 2 - _settings.Offset)
        {
            _player.SetPostion(new Vector2(_player.Position.x, Screen.height / 2 - _settings.Offset));
            AddForce(Vector2.down);
        }
    }
}
