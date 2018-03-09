using System.Collections.ObjectModel;
using System.Windows.Controls;
using Workshop.ModuleA.Model;
using System;
using Microsoft.Practices.Prism.Events;

namespace Workshop.ModuleA.Views
{
    public partial class ProductsListView : UserControl, IProductsListView
    {
        public ProductsListView()
        {
            InitializeComponent();
        }

        public ObservableCollection<Product> Model
        {
            get { return this.DataContext as ObservableCollection<Product>; }
            set { this.DataContext = value; }
        }

        public event EventHandler<DataEventArgs<Product>> ProductSelected = delegate { };

        private void SelectedProductChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Product selectedProduct = e.AddedItems[0] as Product;
                if (selectedProduct != null)
                {
                    RaiseProductSelected(selectedProduct);
                }
            }
        }

        private void RaiseProductSelected(Product product)
        {
            EventHandler<DataEventArgs<Product>> handler = this.ProductSelected;
            if (handler != null)
            {
                handler(this, new DataEventArgs<Product>(product));
            }
        }
    }
}
