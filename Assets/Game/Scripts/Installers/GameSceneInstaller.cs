using Game.Scripts.RailsBuildingSystem;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Toggle>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<BuildingRailsFeature>().AsSingle();

            Container.Bind<ValidationScreen>().FromComponentInHierarchy().AsSingle();
        }
    }
}