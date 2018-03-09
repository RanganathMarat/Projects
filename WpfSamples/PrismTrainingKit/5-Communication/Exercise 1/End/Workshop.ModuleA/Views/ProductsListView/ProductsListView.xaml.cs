using System.Collections.ObjectModel;
using System.Windows.Controls;
using Workshop.ModuleA.Model;
using System;
using Microsoft.Practices.Prism.Events;

namespace Workshop.ModuleA.Views.ProductsListView
{
    public partial class ProductsListView : UserControl, IProductsListView
    {
        public ProductsListView(IProductsListViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
