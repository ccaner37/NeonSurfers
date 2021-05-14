using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller<DefaultInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().To<GameManager>().AsSingle();
    }
}