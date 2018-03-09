using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Workshop.Infrastructure.Services;
using Workshop.ModuleB.Views;
using Workshop.ModuleB.Views.ProductProvidersView;

namespace Workshop.ModuleB
{
    public class ModuleB : IModule
    {

        private readonly IUnityContainer unityContainer;
        private readonly IUIMessagesService messagesService;

        public ModuleB(IUnityContainer unityContainer, IUIMessagesService messagesService) 
        {
            this.unityContainer = unityContainer;
            this.messagesService = messagesService;
        }

        public void Initialize()
        {
            this.messagesService.ShowMessage("Module B Initialized");

            this.unityContainer.RegisterType<IProductProvidersView, ProductProvidersView>();

            this.unityContainer.Resolve<ProvidersController>();
        }
    }
}
