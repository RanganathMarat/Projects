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
using Workshop.ModuleA.Services;
using Microsoft.Practices.Prism.Commands;
using Workshop.Infrastructure.Services;
using Workshop.Infrastructure.Model;

namespace Workshop.ModuleA.Views.ProductsListView
{
    public class ProductsListViewModel : IProductsListViewModel
    {
        private readonly IProductsController productsController;
        
        public ProductsListViewModel(IProductsService productsService, IProductsController productsController)
        {
            this.Products = productsService.GetProducts();
            this.productsController = productsController;
            this.SelectedItemCommand = new DelegateCommand<Product>(this.ExecuteSelectedItem);
        }
        
        public ObservableCollection<Product> Products { get; set; }

        public DelegateCommand<Product> SelectedItemCommand { get; set; }

        private void ExecuteSelectedItem(Product product) 
        {
            this.productsController.OnProductSelected(product);
        }
    }
}
