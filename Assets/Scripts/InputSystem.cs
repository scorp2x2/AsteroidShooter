using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystem : MonoBehaviour
{
    Player _player;
    [SerializeField] InputState _inputState;

    [Inject]
    public void Construct(InputState inputState, Player player)
    {
        _player = player;
        _inputState = inputState;
    }

    public void OnLeft(InputValue input)
    {
        _inputState.Left = input.isPressed;
    }

    public void OnRight(InputValue input)
    {
        _inputState.Right = input.isPressed;
    }

    public void OnUp(InputValue input)
    {
        _inputState.Up = input.isPressed;
    }

    public void OnDown(InputValue input)
    {
        _inputState.Down = input.isPressed;
    }

    public void OnGun(InputValue input)
    {
        if (_inputState.ShootLaser)
        {
            _inputState.ShootGun = false;
            return;
        }

        _inputState.ShootGun = input.isPressed;
    }

    public void OnLaser(InputValue input)
    {
        if (_inputState.ShootGun)
        {
            _inputState.ShootLaser = false;
            return;
        }

        _inputState.ShootLaser = input.isPressed;
    }
}
