using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyProperty
{

    //Metadata
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


    //representer -> Datatype of a Property. DependencyObject represents the datatype of a property.
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

        public static DependencyProperty Register(Type propertyType, PropertyMetadata propertyMetadata)
        {
            DependencyProperty dpInstance = new DependencyProperty();
            dpInstance.propertyType = propertyType;
            dpInstance.propertyMetadata = propertyMetadata;
            if (propertyMetadata.DefaultValue.GetType() != propertyType)
                throw new Exception();
            return dpInstance;
        }
    }

    public class DependencyObject
    {
        Dictionary<DependencyProperty, object> store = new Dictionary<DependencyProperty, object>();
        public object GetValue(DependencyProperty property)
        {
            object effectiveValue = property.PropertyMetadata.DefaultValue;
            if (store.ContainsKey(property))
                effectiveValue = store[property];
            return effectiveValue;
        }
        public void SetValue(DependencyProperty property, object objectValue)
        {
            store[property] = objectValue;
        }
    }

    class Para: DependencyObject
    {
        public static DependencyProperty WidthProperty = DependencyProperty.Register(typeof(double), new PropertyMetadata(100d));
        

        public double Width
        {
            get { return Convert.ToDouble(GetValue(WidthProperty)); }
            set { SetValue(WidthProperty, value); }
        }
        string colour;

        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Para p = new Para();
            Para p1 = new Para();
            p.Width = 32.3;
            p1.Width = 66.6;

            Console.WriteLine(p.Width);
            Console.WriteLine(p1.Width);
            Console.WriteLine(Para.WidthProperty);
        }
    }
}
