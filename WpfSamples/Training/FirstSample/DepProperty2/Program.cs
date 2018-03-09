using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepProperty2
{
    public class PropertyMetadata
    {
        object defaultValue;

        public object DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
        public PropertyMetadata(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }
    }


    public class DependencyProperty
    {
        Type propertyType;
        PropertyMetadata propertyMetadata;

        public PropertyMetadata PropertyMetadata
        {
            get { return propertyMetadata; }
            set { propertyMetadata = value; }
        }
        private DependencyProperty() { }

        public static DependencyProperty Register(string name, Type propertyType, Type ownerTpe, PropertyMetadata propertyMetadata)
        {
            DependencyProperty dpInstance = new DependencyProperty();
            dpInstance.propertyType = propertyType;
            dpInstance.propertyMetadata = propertyMetadata;
            if (propertyMetadata.DefaultValue.GetType() != propertyType)
                throw new Exception();
            return dpInstance;
        }
    }

    class DependencyObject
    {
        static Dictionary<DependencyProperty, object> store = new Dictionary<DependencyProperty, object>();
        public object GetValue(DependencyProperty property)
        {
            object effectiveValue = property.PropertyMetadata.DefaultValue;
            if (store.ContainsKey(property))
            {
                effectiveValue = store[property];
            }

            FrameworkElement fe = this as FrameworkElement;
            Style style = fe.style;
            if (style != null)
            {
                var setter = style.Setters.Where(s => s.DepProperty == property).FirstOrDefault();
                if (setter != null)
                    effectiveValue = setter.Value;
            }

            return effectiveValue;
        }
        public void SetValue(DependencyProperty property, object objectValue)
        {
            store[property] = objectValue;
        }
    }

    class FrameworkElement : DependencyObject
    {
        public Style style {get; set;}
    }


    class Para : FrameworkElement
    {
        public static DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(Para), new PropertyMetadata(100d));

        public double Width
        {
            get { return Convert.ToDouble(GetValue(WidthProperty)); }
            set { SetValue(WidthProperty, value);}
        }
        
    }

    class Style
    {
        public Style()
        {
            List<Setter> Setters = new List<Setter>(); 
        }
        public List<Setter> Setters = null;


    public class Setter
    {
        public Setter() { }

        public DependencyProperty DepProperty { set; get; }
        public object Value { set; get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Para p = new Para();
 
            Style style = new Style();
            Setter setter = new Setter() { DepProperty = Para.WidthProperty, Value = 100d };
            style.Setters.Add(setter);
            p.style = style;

            p.Width = 32.3;
            
            Console.WriteLine(p.Width);
        }
        }
    }
}
