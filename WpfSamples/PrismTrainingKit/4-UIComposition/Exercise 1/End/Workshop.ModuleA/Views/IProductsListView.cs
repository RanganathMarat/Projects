using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Views
{
    public interface IProductsListView
    {
        ObservableCollection<Product> Model { get; set; }
    }   
}
