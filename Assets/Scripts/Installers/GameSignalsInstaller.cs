using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AsteroidDeadSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
        Container.DeclareSignal<StartGameSignal>();
    }
}
