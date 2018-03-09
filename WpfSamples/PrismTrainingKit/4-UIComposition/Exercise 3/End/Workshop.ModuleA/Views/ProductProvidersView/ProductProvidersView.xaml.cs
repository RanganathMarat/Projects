using System.Windows.Controls;
using System.Collections.ObjectModel;
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Views.ProductProvidersView
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
