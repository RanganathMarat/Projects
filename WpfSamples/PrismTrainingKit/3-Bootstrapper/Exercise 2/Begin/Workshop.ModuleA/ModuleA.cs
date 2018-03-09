using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Workshop.Infrastructure.Services;
using Microsoft.Practices.Unity;

namespace Workshop.ModuleA
{
    public class ModuleA : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUIMessagesService uiMessagesService;

        /// <summary>
        /// Constructor for Module A
        /// </summary>
        /// <param name="regionManager">Unity will inject an instance of the container. IRegionManager is registered in the container in the bootstrapper of the application.</param>
        /// <param name="uiMessagesService">Unity will inject an instance of the container. The UIMessagesService is registered by the UIMessagesModule.</param>
        public ModuleA(IRegionManager regionManager, IUIMessagesService uiMessagesService)
        {
            this.regionManager = regionManager;

            // Get an instance of the UIMessageService
            this.uiMessagesService = uiMessagesService;
        }           
        
        public void Initialize()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "I'm in the Shell";
            this.regionManager.Regions["MainRegion"].Add(textBlock);

            // Use the UIMessagesService to show a message
            this.uiMessagesService.ShowMessage("Module A initialized");
        }
    }
}
