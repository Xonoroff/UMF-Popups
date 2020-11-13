using PopupsModule.src.Feature.Managers;
using Scripts.src.Feature.Entities;
using Scripts.src.Feature.Storage;
using UnityEngine;
using Zenject;

namespace Scripts.src.Feature.Installers
{
    public class PopupsModuleInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PopupsModuleConfig>().FromResource(PopupsModuleConfig.FILE_NAME).AsTransient();
            Container.Bind<GameObject>().WithId("PopupsCanvas").FromResolveGetter<PopupsModuleConfig>((x)=> x.GetCanvas()).AsCached();
            Container.Bind<PopupsViewManager>().FromNewComponentOnNewGameObject().AsCached();
            Container.Bind<IPopupsModuleStorage>().To<PopupsModuleStorage>().AsCached();
            Container.Bind<IPopupsManager>().To<PopupsManager>().AsTransient();
        }
    }
}
