using Workshop.Infrastructure.Model;
using Microsoft.Practices.Prism.Events;

namespace Workshop.ModuleA.Views.ProductDetailsView
{
    public class ProductDetailsPresenter
    {
        public ProductDetailsPresenter(IProductDetailsView view)
        {
            this.View = view;
            
        }

        public IProductDetailsView View { get; set; }

        public void SetProduct(Product product)
        {
            this.View.Model = product;
        }        
    }
}
