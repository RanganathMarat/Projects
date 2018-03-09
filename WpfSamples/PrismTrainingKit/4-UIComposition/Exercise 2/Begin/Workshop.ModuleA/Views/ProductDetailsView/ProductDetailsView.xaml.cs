using System.Windows.Controls;
using Workshop.ModuleA.Model;

namespace Workshop.ModuleA.Views.ProductDetailsView
{
    public partial class ProductDetailsView : UserControl, IProductDetailsView
    {
        public ProductDetailsView()
        {
            InitializeComponent();
        }

        public Product Model
        {
            get { return this.DataContext as Product; }
            set { this.DataContext = value; }
        }
    }
}
