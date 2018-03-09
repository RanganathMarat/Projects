using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AttachedPropertyDemo
{

    public class UIElement: DependencyObject
    {
       //public Dictionary<string, object> unknownMembers = new Dictionary<string, object>();

        public string Name { get; set; }
        //public int RowNumber { get; set; }
        //public int ColumnNumber { get; set; }
    }

    public class Grid
    {
        // 
        public static DependencyProperty RowProperty = DependencyProperty.RegisterAttached("Row", typeof(int), typeof(Grid));
        public static DependencyProperty ColProperty = DependencyProperty.RegisterAttached("Col", typeof(int), typeof(Grid));
        public Grid()
        {
            children = new List<UIElement>();
        }
        List<UIElement> children;

        public List<UIElement> Children
        {
            get { return children; }
            set { children = value; }
        }
        public void Arrange()
        {
            foreach (UIElement child in Children)
            {
                int rowIndex = GetRow(child);
                int columnIndex = GetColumn(child);
                Console.WriteLine("The child {0} is positioned at Row {1} and Column {2}", child.Name, GetRow(child), GetColumn(child));
            }
        }

        public static void SetRow(UIElement child, int value)
        {
            child.SetValue(RowProperty, value);
        }

        public static int GetRow(UIElement child)
        {
            return (int)child.GetValue(RowProperty);
            
        }
        public static void SetColumn(UIElement child, int value)
        {
            child.SetValue(ColProperty, value);
        }
        public static int GetColumn(UIElement child)
        {
            return (int)child.GetValue(ColProperty);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Grid g = new Grid();
            UIElement child1 = new UIElement() { Name = "Button1" };
            UIElement child2 = new UIElement() { Name = "Button2" };
            UIElement child3 = new UIElement() { Name = "Button3" };
            Grid.SetRow(child1, 1); Grid.SetColumn(child1, 1);
            Grid.SetRow(child2, 2); Grid.SetColumn(child2, 2); 
            Grid.SetRow(child3, 3); Grid.SetColumn(child3, 3);
            
            g.Children.Add(child1);
            g.Children.Add(child2);
            g.Children.Add(child3);
            g.Arrange();
            
        }
    }
}
