using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace EmptyStart
{

    class App : Application
    {
        public void InitializeComponent()
        {
            this.StartupUri = new Uri("MainWindow.xaml", UriKind.RelativeOrAbsolute);
        }
    }

    class BootStraper
    {
        [STAThread]
        public static void Main()
        {
            //Label label = new Label() { Height = 30, Width = 100 };
            //label.Content = "Randomness";
            //TextBox textbox = new TextBox() { Height = 30, Width = 100 };
            //Button button = new Button() { Height = 30, Width = 100 };
            //button.Content = "Search";

            //StackPanel stackPanel = new StackPanel();
            //stackPanel.Children.Add(label);
            //stackPanel.Children.Add(textbox);
            //stackPanel.Children.Add(button);

            //Window window = new Window();
            //window.Content = stackPanel;

            //Try de-serializing the XML content into a Window object
            //Window XmlWindow = (Window)(System.Windows.Markup.XamlReader.Load(System.Xml.XmlReader.Create("..\\..\\CopyMainWindow.xml")));

            //Application wpfInstance = new Application();
            //wpfInstance.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            //wpfInstance.Run(window);
            //wpfInstance.Run(XmlWindow);
            //wpfInstance.Run();
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
