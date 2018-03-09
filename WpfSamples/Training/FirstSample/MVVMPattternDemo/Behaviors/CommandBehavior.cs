using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMPattternDemo.Behaviors
{
    public class CommandBehavior
    {
        public static DependencyProperty commandProperty = 
            DependencyProperty.RegisterAttached("Command", 
            typeof(System.Windows.Input.ICommand), 
            typeof(CommandBehavior),
            new PropertyMetadata(OnCommandValueChanged));

        public static void OnCommandValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            RoutedEvent e = GetEventName(source);
            if (e != null)
            {
                UIElement elem = source as UIElement;
                if (elem != null)
                {
                    elem.AddHandler(e, new RoutedEventHandler(Handler));
                }
            }
        }

        public static void Handler(object source, RoutedEventArgs e)
        {
            ICommand com = GetCommand(source as DependencyObject);
            if( com != null )
            {
                if( com.CanExecute(GetCommandParameter(source as DependencyObject)))
                {
                    com.Execute(GetCommandParameter(source as DependencyObject));
                }
            }
        }

        public static void SetCommand(DependencyObject source, System.Windows.Input.ICommand value)
        {
            source.SetValue(commandProperty, value);
        }

        public static System.Windows.Input.ICommand GetCommand(DependencyObject source)
        {
            return (System.Windows.Input.ICommand)source.GetValue(commandProperty);
        }


        public static DependencyProperty EventNameProperty = 
            DependencyProperty.RegisterAttached("EventName", typeof(RoutedEvent), typeof(CommandBehavior));

        public static void SetEventName(DependencyObject source, RoutedEvent value)
        {
            source.SetValue(EventNameProperty, value);
        }

        public static RoutedEvent GetEventName(DependencyObject source)
        {
            return (RoutedEvent)source.GetValue(EventNameProperty);
        }

        public static DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(CommandBehavior));

        public static void SetCommandParameter(DependencyObject source, object value)
        {
            source.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(DependencyObject source)
        {
            return source.GetValue(CommandParameterProperty);
        }
    }
}
