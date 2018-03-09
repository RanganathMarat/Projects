using System.Collections.ObjectModel;
using Workshop.Infrastructure.Model;

namespace Workshop.Infrastructure.Services
{
    public interface IProductsService
    {
        ObservableCollection<Product> GetProducts();

        ObservableCollection<Provider> GetProviders(int productId);
    }
}
