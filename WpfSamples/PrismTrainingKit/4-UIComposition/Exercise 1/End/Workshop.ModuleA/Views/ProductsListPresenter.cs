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
using Workshop.ModuleA.Services;

namespace Workshop.ModuleA.Views
{
    public class ProductsListPresenter
    {
        /// <summary>
        /// Constructor for the ProductsListPresenter using Presenter-First approach.
        /// </summary>
        /// <param name="view">An instance of the ProductListView. Using Presenter-First approach, so the presentar has a references to the View.</param>
        /// <param name="productsService">An instance of the ProductService. The ProductServices is registered in the container by ModuleA</param>
        public ProductsListPresenter(IProductsListView view, IProductsService productsService)
        {
            this.View = view;

            // retrieve products list
            var products = productsService.GetProducts();

            // set the view's model
            this.View.Model = products;
        }

        public IProductsListView View { get; set; }
    }
}
