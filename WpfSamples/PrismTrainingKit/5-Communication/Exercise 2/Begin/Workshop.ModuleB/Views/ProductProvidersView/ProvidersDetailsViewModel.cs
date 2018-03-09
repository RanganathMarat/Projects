using System.Collections.ObjectModel;
using Workshop.Infrastructure.Model;

namespace Workshop.ModuleB.Views.ProductProvidersView
{
    public class ProvidersDetailsViewModel
    {
        public ObservableCollection<Provider> Providers { get; set; }

        public string Title { get; set; }
    }
}
