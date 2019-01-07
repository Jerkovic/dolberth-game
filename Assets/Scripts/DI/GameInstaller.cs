using Dolberth.Managers.GameManager;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle().NonLazy();
        Debug.Log("Install DI-bindings");
    }
}