﻿using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Services
{
    public class ProductsService : IProductsService
    {
        public ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            int nextProductId = 1;

            products.Add(new Product(nextProductId++) { Name = "Bike", Price = 100 });
            products.Add(new Product(nextProductId++) { Name = "Mercedez Car", Price = 9700 });
            products.Add(new Product(nextProductId++) { Name = "BMW Car", Price = 5600 });
            products.Add(new Product(nextProductId++) { Name = "Ford Car", Price = 4600 });

            return products;
        }
    }
}
