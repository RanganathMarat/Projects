using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Views.ProductProvidersView
{
    public class ProvidersDetailsViewModel
    {
        public ObservableCollection<Provider> Providers { get; set; }

        public string Title { get; set; }
    }
}
