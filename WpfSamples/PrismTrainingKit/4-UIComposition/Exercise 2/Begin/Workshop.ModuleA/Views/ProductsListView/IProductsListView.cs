using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;
using Microsoft.Practices.Prism.Events;
using System;

namespace Workshop.ModuleA.Views
{
    public interface IProductsListView
    {
        ObservableCollection<Product> Model { get; set; }
    }   
}
