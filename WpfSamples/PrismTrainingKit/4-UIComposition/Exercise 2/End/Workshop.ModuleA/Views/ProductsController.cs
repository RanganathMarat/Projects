using Microsoft.Practices.Prism.Regions;
using Workshop.ModuleA.Views.ProductDetailsView;
using Workshop.ModuleA.Model;
using Microsoft.Practices.Unity;
namespace Workshop.ModuleA.Views
{
    public class ProductsController : IProductsController
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public ProductsController(IUnityContainer container,IRegionManager regionManger)
        {
            this.regionManager = regionManger;
            this.container = container;
        }
        
        public void OnProductSelected(Product product)
        {
            // Get the Details region (View Injection)
            var region = this.regionManager.Regions["DetailsRegion"];

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

                // Add the view using its name
                region.Add(productDetailsView, viewName);
            }

            // Activate the view
            region.Activate(productDetailsView);
        }        
    }
}
