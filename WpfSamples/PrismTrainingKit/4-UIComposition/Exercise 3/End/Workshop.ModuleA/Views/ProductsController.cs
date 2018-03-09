using Microsoft.Practices.Prism.Regions;
using Workshop.ModuleA.Views.ProductDetailsView;
using Workshop.ModuleA.Model;
using Microsoft.Practices.Unity;
using Workshop.ModuleA.Views.ProductProvidersView;
namespace Workshop.ModuleA.Views
{
    public class ProductsController : IProductsController
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public ProductsController(IUnityContainer container, IRegionManager regionManger)
        {
            this.regionManager = regionManger;
            this.container = container;
        }

        public void OnProductSelected(Product product)
        {
            // Get the Bottom region (View Injection)
            var region = this.regionManager.Regions["BottomRegion"];

            // we will name views to register and retrieve them (using the product id). This avoids having several view instances for the same product.
            var viewName = product.ProductId.ToString();

            // Get a view from the region using the view's name.
            var productDetailsView = region.GetView(viewName);

            // If the view was never created, create it for the first time
            if (productDetailsView == null)
            {
                // Resolve the presenter and set the product
                var presenter = this.container.Resolve<ProductDetailsPresenter>();
                presenter.SetProduct(product);

                productDetailsView = presenter.View;

                // Add the view using its name. As the view contains a region, create a ScopedRegionManager.                
                var bottomRegionManager = region.Add(productDetailsView, viewName, true);

                // Add providers View to the details
                this.AddProviderDetailsView(product, bottomRegionManager);
            }

            // Activate the view
            region.Activate(productDetailsView);
        }

        private void AddProviderDetailsView(Product product, IRegionManager regionManager)
        {
            // Get the Details regions
            var region = regionManager.Regions["DetailsRegion"];

            // Resolve the presenter and set the product
            var presenter = this.container.Resolve<ProductProvidersPresenter>();
            presenter.SetProduct(product);

            // Add the view using its name
            region.Add(presenter.View);
        }
    }
}
