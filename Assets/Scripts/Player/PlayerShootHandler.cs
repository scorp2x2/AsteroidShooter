using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerShootHandler : ITickable
{
    Settings _settings;
    Player _player;
    InputState _inputState;
    BulletGun.Factory _bulletGunFactory;
    BulletLaser.Factory _bulletLaserFactory;

    [Serializable]
    public class Settings
    {
        public float BulletSpeed;
        public float ShootInterval;

        public float damageGun;
        public float damageLaser;
    }

    public PlayerShootHandler(InputState inputState, Player player, Settings settings, BulletGun.Factory factoryGun, BulletLaser.Factory factoryLaser)
    {
        _settings = settings;
        _player = player;
        _inputState = inputState;
        _bulletGunFactory = factoryGun;
        _bulletLaserFactory = factoryLaser;
    }

    float timeLastShoot;
    BulletLaser laser;

    public void Tick()
    {
        if (_player.IsDead) return;

        if (_inputState.ShootGun && timeLastShoot + _settings.ShootInterval < Time.time)
        {
            timeLastShoot = Time.time;
            var bullet = _bulletGunFactory.Create(_settings.BulletSpeed, _settings.damageGun);
            bullet.transform.localPosition = _player.Position;
        }

        if (_inputState.ShootLaser)
        {
            if (laser == null || !laser.gameObject.activeSelf)
                laser = _bulletLaserFactory.Create(_settings.BulletSpeed, _settings.damageLaser, _player);
        }
    }
}
