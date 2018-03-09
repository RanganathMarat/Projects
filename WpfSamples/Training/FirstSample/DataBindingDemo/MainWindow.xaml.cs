using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBindingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Binding connnector = new Binding();
            //connnector.Source = this.Slider;
            ////SOurce property can be a CLR property or a dependency property.
            //connnector.Path = new PropertyPath("Value");
            //this.Strenght.SetBinding(Slider.FontSizeProperty, connnector);

        }
    }
}
