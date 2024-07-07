using Game.Scripts.RailsBuildingSystem;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public BuilderConfiguration builderConfiguration;
        public override void InstallBindings()
        {
            Container.BindInstance(builderConfiguration);
        }
    }
}