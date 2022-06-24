using System;
using UnityEditor;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettings : ScriptableObjectInstaller<GameSettings>
{
    [Header("��������� ��� ������")]
    public PlayerSettings Player;
    [Header("��������� ��� ����������")]
    public AsteroidSettings Asteroid;

    [Header("�������� ��������")]
    public GameInstaller.Settings GISettings;

    public override void InstallBindings()
    {
        Container.BindInstance(Player.MoveHandler).IfNotBound();
        Container.BindInstance(Player.ShootHandler).IfNotBound();
        Container.BindInstance(Player.Settings).IfNotBound();

        Container.BindInstance(Asteroid.SpawnSettings).IfNotBound();

        Container.BindInstance(GISettings).IfNotBound();
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMoveHandler.Settings MoveHandler;
        public PlayerShootHandler.Settings ShootHandler;
        public Player.Settings Settings;
    }

    [Serializable]
    public class AsteroidSettings
    {
        public Spawner.Settings SpawnSettings;
    }
}