using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
namespace MVVMPattternDemo
{

    //public class Control
    //{
    //    public virtual void OnClick(object sender, EventArgs args) {}
    //}

    ////public interface ICommand
    ////{
    ////    bool CanExecute(object parameter);
    ////    void Execute(object parameter);
    ////}

    public interface ICommandSource
    {
        ICommand Command { get; set; }
        object CommandParameter { get; set; }
    }

    class Button: FrameworkElement, ICommandSource
    {

        private static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(Button));

        public static DependencyProperty CommandProperty1
        {
            get { return Button.CommandProperty; }
            set { Button.CommandProperty = value; }
        }

        public event EventHandler Click;

        private object commandParameter;
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty1);
            }
            set
            {
                SetValue(CommandProperty1, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return commandParameter;
            }
            set
            {
                commandParameter = value;
            }
        }

        public void OnClick()
        {
            if (Click != null)
            {
                Click.Invoke(this, new EventArgs());
            }
            if (Command != null)
            {
                if (Command.CanExecute(commandParameter))
                {
                    Command.Execute(commandParameter);
                }
            }
        }
    }

    class View:FrameworkElement
    {
        Button button = new Button();
        //ViewModel vm = new ViewModel();
        public View( object datacontextvalue)
        {
            this.DataContext = datacontextvalue;
            button.DataContext = this.DataContext;
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Save");
            button.SetBinding(Button.CommandProperty1, binding);
        }

        public void SimulateClick()
        {
            button.OnClick();
        }
    }


    // Testability is good here because we can use ViewModel.Save.Execute. 
    class ViewModel:INotifyPropertyChanged
    {
        //private DelegateCommand save = new DelegateCommand(SaveState, canSave);
        private System.Windows.Input.ICommand save;

        public System.Windows.Input.ICommand Save
        {
            get {  return save; }
            set { save = value; }
        }

        private string searchKey;

        public string SearchKey
        {
            get { return searchKey; }
            set { searchKey = value; OnPropertyChanged("SearchKey"); }
        }

        private string searchResult;

        public string SearchResult
        {
            get { return searchResult; }
            set { searchResult = value; OnPropertyChanged("SearchResult");}
        }

        public ViewModel()
        {
            save = new DelegateCommand(obj => { SaveState(obj); }, obj => { return CanSave(obj); });
        }

        public bool CanSave(object parameter)
        {
            return true;
        }

        public void SaveState(object parameter)
        {
            SearchResult = SearchKey.ToUpper(); 
            MessageBox.Show("The Actual save logic....\n");
            if (parameter != null)
            {
                MessageBox.Show(parameter as string);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if( PropertyChanged !=null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    class DelegateCommand : System.Windows.Input.ICommand
    {
        Func<object, bool> canExecuteAddress;
        Action<object> executeAddress;

        public DelegateCommand(Action<object> executeAddress, Func<object, bool> canExecuteAddress)
        {
            this.canExecuteAddress = canExecuteAddress;
            this.executeAddress = executeAddress;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteAddress(parameter);
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("Calling Save.....");
            executeAddress(parameter);
        }


        public event EventHandler CanExecuteChanged;
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //View v = new View(new ViewModel());
            //v.SimulateClick();
            Application app = new Application();
            app.StartupUri = new Uri("Shell.xaml", UriKind.RelativeOrAbsolute);
            app.Run();

            // TO DO - Create a View and View model should be the datacontext provided to view.
            // Create a derived windows class. Create a stackpanel - a button and a text box.
            // Viewmodel should have the Commands and the data binding.
            // What is entered in the text box 
        }
    }
}
