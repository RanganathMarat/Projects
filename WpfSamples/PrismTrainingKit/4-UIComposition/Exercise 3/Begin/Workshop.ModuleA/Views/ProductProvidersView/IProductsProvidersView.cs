using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;
namespace Workshop.ModuleA.Views.ProductProvidersView
{
    public interface IProductProvidersView
    {
        ProvidersDetailsViewModel Model { get; set; }
    }
}
