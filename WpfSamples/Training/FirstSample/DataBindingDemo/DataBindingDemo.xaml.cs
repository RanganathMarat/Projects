using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBindingDemo
{
    /// <summary>
    /// Interaction logic for DataBindingDemo.xaml
    /// </summary>
    public partial class DataBindingDemo : Window
    {
        Model.DataModel datamodel;

        public Model.DataModel Datamodel
        {
            get { return datamodel; }
            set { datamodel = value; }
        }

        public DataBindingDemo()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            datamodel = new Model.DataModel() { State = "DefaultState", Number=12};
            //this.DataContext = datamodel;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            datamodel.State = "ModifiedState";
            MessageBox.Show(datamodel.State);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(datamodel.State);
            
        }
    }
}
