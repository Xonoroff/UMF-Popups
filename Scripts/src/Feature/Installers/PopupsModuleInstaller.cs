using PopupsModule.src.Feature.Managers;
using UnityEngine;
using Zenject;

namespace PopupsModule.src.Feature.Installers
{
    public class PopupsModuleInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PopupsViewManager>().FromNewComponentOnNewGameObject().AsCached();
            Container.Bind<GameObject>().WithId("PopupsCanvas").FromResource("PopupsCanvas").AsCached();
        }
    }
}
