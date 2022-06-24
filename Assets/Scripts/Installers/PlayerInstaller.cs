using System;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public Settings _settings;

    [Serializable]
    public class Settings
    {
        public Rigidbody2D Rigidbody;
    }

    public override void InstallBindings()
    {
        Container.Bind<Player>().AsSingle().WithArguments(_settings.Rigidbody);
        Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
        Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();
    }
}