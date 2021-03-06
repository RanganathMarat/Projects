﻿using System;
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
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Services
{
    public class ProductsService : IProductsService
    {
        public ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            int nextProductId = 1;

            products.Add(new Product(nextProductId++) { Name = "Bike", Price = 100, Description = "Road bicycle, available in Black, White and Red", Stock = 25});
            products.Add(new Product(nextProductId++) { Name = "Mercedez Car", Price = 9700, Description = "A Class Mercedez Benz, available in Red and Black", Stock = 3 });
            products.Add(new Product(nextProductId++) { Name = "BMW Car", Price = 5600, Description = "Brand new BMW car, 1 Series, 3 Series, 5 Series, 6 Series available", Stock = 5 });
            products.Add(new Product(nextProductId++) { Name = "Ford Car", Price = 4600, Description = "Low mileage Used Ford car", Stock = 1 });

            return products;
        }
    }
}
