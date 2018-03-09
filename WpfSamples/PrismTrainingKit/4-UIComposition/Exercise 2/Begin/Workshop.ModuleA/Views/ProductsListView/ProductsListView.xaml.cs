﻿using System.Collections.ObjectModel;
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
    }
}
