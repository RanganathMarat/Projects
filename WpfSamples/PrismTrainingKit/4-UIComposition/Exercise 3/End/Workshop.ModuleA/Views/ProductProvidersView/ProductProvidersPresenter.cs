using Workshop.ModuleA.Model;
using Workshop.ModuleA.Services;

namespace Workshop.ModuleA.Views.ProductProvidersView
{
    public class ProductProvidersPresenter
    {
        private IProductsService productsService;

        public ProductProvidersPresenter(IProductsService productsService, IProductProvidersView view)
        {
            this.productsService = productsService;
            this.View = view;
        }

        public IProductProvidersView View { get; set; }

        public void SetProduct(Product product)
        {
            // Create a view model wrapping the actual model, to provide additional fields to the view (in this case the Title, that is used by the TabControl to render its header)
            var viewModel = new ProvidersDetailsViewModel();
            viewModel.Providers = productsService.GetProviders(product.ProductId);
            viewModel.Title = "Providers";

            this.View.Model = viewModel;
        }

    }
}
