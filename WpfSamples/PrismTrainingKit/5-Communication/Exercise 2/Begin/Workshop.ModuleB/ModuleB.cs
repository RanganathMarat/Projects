using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Workshop.Infrastructure.Services;

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
        }
    }
}
