using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.Windows;

namespace Workshop.Infrastructure.Behaviors
{
    public static class DataGridSelectedItem
    {
        private static readonly DependencyProperty DataGridSelectedItemCommandBehaviorProperty
            = DependencyProperty.RegisterAttached(
            "DataGridSelectedItemCommandBehavior",
            typeof(DataGridSelectedItemCommandBehavior),
            typeof(DataGridSelectedItem),
            null);

        public static readonly DependencyProperty CommandProperty
            = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(DataGridSelectedItem),
            new PropertyMetadata(OnSetCommandCallback));

        public static readonly DependencyProperty CommandParameterProperty
            = DependencyProperty.RegisterAttached(
           "CommandParameter",
           typeof(object),
           typeof(DataGridSelectedItem),
           new PropertyMetadata(OnSetCommandParameterCallback));
            
        public static ICommand GetCommand(DataGrid control)
        {
            return control.GetValue(CommandProperty) as ICommand;
        }

        public static void SetCommand(DataGrid control, ICommand command)
        {
            control.SetValue(CommandProperty, command);
        }

        public static void SetCommandParameter(DataGrid control, object parameter)
        {
            control.SetValue(CommandParameterProperty, parameter);
        }

        public static object GetCommandParameter(DataGrid control)
        {
            return control.GetValue(CommandParameterProperty);
        }

        private static void OnSetCommandCallback
            (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DataGrid control = dependencyObject as DataGrid;
            if (control != null)
            {
                DataGridSelectedItemCommandBehavior behavior = GetOrCreateBehavior(control);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback
            (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DataGrid control = dependencyObject as DataGrid;
            if (control != null)
            {
                DataGridSelectedItemCommandBehavior behavior = GetOrCreateBehavior(control);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static DataGridSelectedItemCommandBehavior GetOrCreateBehavior(DataGrid control)
        {
            DataGridSelectedItemCommandBehavior behavior =
                control.GetValue(DataGridSelectedItemCommandBehaviorProperty) as DataGridSelectedItemCommandBehavior;
            if (behavior == null)
            {
                behavior = new DataGridSelectedItemCommandBehavior(control);
                control.SetValue(DataGridSelectedItemCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }

    public class DataGridSelectedItemCommandBehavior : CommandBehaviorBase<DataGrid>
    {
        public DataGridSelectedItemCommandBehavior(DataGrid control)
            : base(control)
        {
            control.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.CommandParameter = e.AddedItems[0];    
            }
            
            ExecuteCommand();
        }
    }
}
