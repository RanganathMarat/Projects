using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Workshop.Infrastructure.Services;
using Workshop.ModuleA.Services;
using Workshop.ModuleA.Views;
using Workshop.ModuleA.Views.ProductDetailsView;
using Workshop.ModuleA.Views.ProductProvidersView;
using Workshop.ModuleA.Views.ProductsListView;

namespace Workshop.ModuleA
{
    public class ModuleA : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUIMessagesService uiMessagesService;
        private readonly IUnityContainer container;

        /// <summary>
        /// Constructor for Module A
        /// </summary>
        /// <param name="regionManager">Unity will inject an instance of the container. IRegionManager is registered in the container in the bootstrapper of the application.</param>
        /// <param name="uiMessagesService">Unity will inject an instance of the MessagesService. The UIMessagesService is registered by the UIMessagesModule.</param>
        /// <param name="container">Unity will inject an instance of the container. The UnityContainer is registered by the application bootstrapper.</param>
        public ModuleA(IRegionManager regionManager, IUIMessagesService uiMessagesService, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;

            // Get an instance of the UIMessageService
            this.uiMessagesService = uiMessagesService;
        }           
        
        public void Initialize()
        {
            // Use the UIMessagesService to show a message
            this.uiMessagesService.ShowMessage("Module A initialized");

            // Register the neccessary services
            this.RegisterServices();

            // Register the neccessary views
            this.RegisterViews();

            // Add the List view to the main region
            ShowListView();

            // Add the Details view to the details region
            //ShowDetailsView();
        }

        private void ShowListView()
        {
            // Get an instance of the ProductsListPresenter using the container
            ProductsListPresenter presenter = this.container.Resolve<ProductsListPresenter>();

            // Get the main region
            IRegion mainRegion = this.regionManager.Regions["MainRegion"];

            // Add the Product List view to the main region (View Injection)
            mainRegion.Add(presenter.View);
        }        

        private void RegisterViews()
        {
            // Register views in the container.
            this.container.RegisterType<IProductsListView, ProductsListView>();
            this.container.RegisterType<IProductDetailsView, ProductDetailsView>();
            this.container.RegisterType<IProductProvidersView, ProductProvidersView>();
        
            // Register products controller
            this.container.RegisterType<IProductsController, ProductsController>();
        }

        private void RegisterServices()
        {
            // Register the products service in the container as Singleton.
            this.container.RegisterType<IProductsService, ProductsService>(new ContainerControlledLifetimeManager());
        }        
    }
}
