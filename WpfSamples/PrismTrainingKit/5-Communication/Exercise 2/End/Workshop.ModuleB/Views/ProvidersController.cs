using Microsoft.Practices.Prism.Events;
using Workshop.Infrastructure.Events;
using Workshop.ModuleB.Views.ProductProvidersView;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Workshop.Infrastructure.Model;

namespace Workshop.ModuleB.Views
{
    public class ProvidersController
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public ProvidersController(IEventAggregator eventAggregator, IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
            
            ProductSelectedEvent productSelected = eventAggregator.GetEvent<ProductSelectedEvent>();
            productSelected.Subscribe(this.OnProductSelected, true);
        }

        public void OnProductSelected(Product product) 
        {
            string regionManagerName = string.Format("Product{0}BottomRegionManager", product.ProductId);
            IRegionManager bottomRegionManager = container.Resolve<IRegionManager>(regionManagerName);
            string viewName = "ProvidersView";

            if (bottomRegionManager.Regions["DetailsRegion"].GetView(viewName) == null)
            {
                var presenter = this.container.Resolve<ProductProvidersPresenter>();
                presenter.SetProduct(product);
                bottomRegionManager.Regions["DetailsRegion"].Add(presenter.View, viewName);
            }
        }
    }
}
