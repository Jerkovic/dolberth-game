using Dolberth.Managers.GameManager;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle().NonLazy();
        // Container.Bind<IGameManager>().To<GameManager>().AsSingle().NonLazy();

        // Container.Bind<EventManager>().AsSingle().NonLazy();
        // Container.Bind<SoundManager>().AsSingle().NonLazy();
    }
}