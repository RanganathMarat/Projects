using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Workshop.Infrastructure.Services;
using Workshop.Infrastructure.Model;

namespace Workshop.ModuleA.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ObservableCollection<Product> products = PopulateProducts();
        private readonly Dictionary<int, ObservableCollection<Provider>> providers = PopulateProviders();

        public ObservableCollection<Product> GetProducts()
        {
            return this.products;
        }

        public ObservableCollection<Provider> GetProviders(int productId)
        {
            return this.providers[productId];
        }

        private static ObservableCollection<Product> PopulateProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            int nextProductId = 1;

            products.Add(new Product(nextProductId++) { Name = "Bike", Price = 100, Description = "Road bicycle, available in Black, White and Red", Stock = 25 });
            products.Add(new Product(nextProductId++) { Name = "Mercedez Car", Price = 9700, Description = "A Class Mercedez Benz, available in Red and Black", Stock = 3 });
            products.Add(new Product(nextProductId++) { Name = "BMW Car", Price = 5600, Description = "Brand new BMW car, 1 Series, 3 Series, 5 Series, 6 Series available", Stock = 5 });
            products.Add(new Product(nextProductId++) { Name = "Ford Car", Price = 4600, Description = "Low mileage Used Ford car", Stock = 1 });

            return products;
        }

        private static Dictionary<int, ObservableCollection<Provider>> PopulateProviders()
        {
            var providers = new Dictionary<int, ObservableCollection<Provider>>();
            int nextProductId = 1;

            providers.Add(nextProductId++, new ObservableCollection<Provider>()
            {
                new Provider() { Name = "Bike Reseller", Phone = "(425) 454-18890", Address = "13 Northeast 1th Street, San Francisco" },
                new Provider() { Name = "Bike Reseller #2", Phone = "(425) 454-18891", Address = "55 Northwest 11th Street, Redmond" },
            });

            providers.Add(nextProductId++, new ObservableCollection<Provider>()
            {
                new Provider() { Name = "Best Mercedez Benz Cars", Phone = "(444) 411-11190", Address = "1 South Av., Seattle" },
                new Provider() { Name = "Cheap Cars", Phone = "(432) 432-432432", Address = "10 Northwest 11th Street, Dallas" },
                new Provider() { Name = "Expensive Cars", Phone = "(432) 999-999999", Address = "11 Northwest 11th Street, NY" },
            });

            providers.Add(nextProductId++, new ObservableCollection<Provider>()
            {
                new Provider() { Name = "Your BMW Choice", Phone = "(425) 111-111111", Address = "13 CrossRoads Av., Washington DC" },
                new Provider() { Name = "TKB BMW", Phone = "(411) 222-222222", Address = "55 Northwest 11th Street, Redmond" },
            });

            providers.Add(nextProductId++, new ObservableCollection<Provider>()
            {
                new Provider() { Name = "Michel", Phone = "(422) 454-188911", Address = "55 Downtown, Seattle" },                
            });

            return providers;
        }
    }
}
