using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Workshop.UIMessagesModule.Services;

namespace Workshop.UIMessagesModule
{
    public class UIMessagesModule : IModule
    {
        /// <summary>
        /// DI Container. 
        /// Unity will inject an instance of the container. IUnityContainer is registered in the container in the bootstrapper of the application.
        /// </summary>
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            // register the UIMessagesService as singleton
            this.Container.RegisterType<UIMessagesService>(new ContainerControlledLifetimeManager());
        }
    }
}
