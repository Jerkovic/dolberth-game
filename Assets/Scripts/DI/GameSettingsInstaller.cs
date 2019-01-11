using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public APlayer.Settings PlayerSettings;

    public override void InstallBindings()
    {
        Container.BindInstances(PlayerSettings);

    }
}

public class APlayer : ITickable
{
    readonly Settings _settings;

    public APlayer(Settings settings)
    {
        _settings = settings;
    }

    public void Tick()
    {
    }

    [Serializable]
    public class Settings
    {
        public float Speed;
        public float Health;
        public float MaxHealth;
        // add more shit here
    }
}