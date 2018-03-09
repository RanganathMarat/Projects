using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Services
{
    public interface IProductsService
    {
        ObservableCollection<Product> GetProducts();

        ObservableCollection<Provider> GetProviders(int productId);
    }
}
