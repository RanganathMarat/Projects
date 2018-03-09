using System.Windows.Controls;
using System.Collections.ObjectModel;
using Workshop.Infrastructure.Model;
using Workshop.ModuleB.Views.ProductProvidersView;

namespace Workshop.ModuleB.Views.ProductProvidersView
{
    public partial class ProductProvidersView : UserControl, IProductProvidersView
    {
        public ProductProvidersView()
        {
            InitializeComponent();
        }

        public ProvidersDetailsViewModel Model
        {
            get { return this.DataContext as ProvidersDetailsViewModel; }
            set { this.DataContext = value; }
        }
    }
}
