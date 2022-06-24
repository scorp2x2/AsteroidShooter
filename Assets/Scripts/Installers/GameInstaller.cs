using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;

    public Transform PanelEnemy;
    public Transform PanelBullets;

    [Serializable]
    public class Settings
    {
        public GameObject AsteroidPrefab;
        public GameObject BulletPrefab;
        public GameObject LaserPrefab;
    }

    public override void InstallBindings()
    {
        Container.Bind<InputState>().AsSingle();
        Container.Bind<GameStats>().AsSingle().WithArguments("player");    

        Container.BindInterfacesAndSelfTo<Spawner>().AsSingle();

        Container.BindFactory<float, float, BulletGun, BulletGun.Factory>()
                .FromPoolableMemoryPool<float, float, BulletGun, BulletGunPool>(poolBinder => poolBinder
                    .FromComponentInNewPrefab(_settings.BulletPrefab)
                    .UnderTransform(PanelBullets));

        Container.BindFactory<float, float, Player, BulletLaser, BulletLaser.Factory>()
                .FromPoolableMemoryPool<float, float, Player, BulletLaser, BulletLaserPool>(poolBinder => poolBinder
                    .WithInitialSize(1)
                    .FromComponentInNewPrefab(_settings.LaserPrefab)
                    .UnderTransform(PanelBullets));

        Container.BindFactory<float, float, float, Asteroid, Asteroid.Factory>()
                .FromPoolableMemoryPool<float, float, float, Asteroid, AsteroidPool>(poolBinder => poolBinder
                    .FromComponentInNewPrefab(_settings.AsteroidPrefab)
                    .UnderTransform(PanelEnemy));

        GameSignalsInstaller.Install(Container);
    }

    class BulletGunPool : MonoPoolableMemoryPool<float, float, IMemoryPool, BulletGun>
    {
    }

    class BulletLaserPool : MonoPoolableMemoryPool<float, float, Player, IMemoryPool, BulletLaser>
    {
    }

    class AsteroidPool : MonoPoolableMemoryPool<float, float, float, IMemoryPool, Asteroid>
    {
    }
}