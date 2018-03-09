using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;
using Microsoft.Practices.Prism.Events;
using System;

namespace Workshop.ModuleA.Views.ProductsListView
{
    public interface IProductsListView
    {
        ObservableCollection<Product> Model { get; set; }

        event EventHandler<DataEventArgs<Product>> ProductSelected;
    }   
}
