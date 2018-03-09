using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DependencyProperty
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl , INotifyPropertyChanged
    {
        public MyControl()
        {
            InitializeComponent();
            DataContext = new 
            { Self = this };
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string LabelName { 
            get
            {
                return labelName;
            }
            set
            {
                labelName = value;
                NotifyPropertyChanged("LabelName");
                //PropertyChanged(this, new PropertyChangedEventArgs("LabelName"));
            } 
        }
        public string TexBoxContent { 
            get
            {
                return textBoxContent;
             } 
            set
            {
                textBoxContent = value;
                NotifyPropertyChanged("TexBoxContent");
                //PropertyChanged(this, new PropertyChangedEventArgs("TexBoxContent"));
            } 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            LabelName = "newLabelName";
            TexBoxContent = "NewLabelContent";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private string labelName;
        private string textBoxContent;
    }
}
