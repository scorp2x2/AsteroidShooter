using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerUI : MonoBehaviour
{
    Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    public bool IsDead
    {
        get { return _player.IsDead; }
    }

    public void TakeDamage(float damage)
    {
        _player.TakeDamage(damage);
    }
}
